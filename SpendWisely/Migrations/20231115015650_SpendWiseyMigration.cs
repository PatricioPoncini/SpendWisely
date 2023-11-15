using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpendWisely.Migrations
{
    /// <inheritdoc />
    public partial class SpendWiseyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseCategoryModel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    icon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategoryModel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IncomeCategoryModel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    icon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeCategoryModel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseModel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<int>(type: "int", nullable: false),
                    expenseCategoryId = table.Column<int>(type: "int", nullable: false),
                    registrationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExpenseModel_ExpenseCategoryModel_expenseCategoryId",
                        column: x => x.expenseCategoryId,
                        principalTable: "ExpenseCategoryModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncomeModel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    registrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    incomeCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IncomeModel_IncomeCategoryModel_incomeCategoryId",
                        column: x => x.incomeCategoryId,
                        principalTable: "IncomeCategoryModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseModel_expenseCategoryId",
                table: "ExpenseModel",
                column: "expenseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeModel_incomeCategoryId",
                table: "IncomeModel",
                column: "incomeCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseModel");

            migrationBuilder.DropTable(
                name: "IncomeModel");

            migrationBuilder.DropTable(
                name: "ExpenseCategoryModel");

            migrationBuilder.DropTable(
                name: "IncomeCategoryModel");
        }
    }
}
