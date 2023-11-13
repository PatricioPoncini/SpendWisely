using Microsoft.EntityFrameworkCore;
using SpendWisely.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("Data Source=DESKTOP-VAJ5GML;Initial Catalog=spendwisely;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public virtual DbSet<User> UserModel { get; set; }
    public virtual DbSet<Expense> ExpenseModel { get; set; }
    public virtual DbSet<ExpenseCategory> ExpenseCategoryModel { get; set; }
    public virtual DbSet<Income> IncomeModel { get; set; }
    public virtual DbSet<IncomeCategory> IncomeCategoryModel { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<User>().Property(t => t.id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Expense>().Property(t => t.id).ValueGeneratedOnAdd();
        modelBuilder.Entity<ExpenseCategory>().Property(t => t.id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Income>().Property(t => t.id).ValueGeneratedOnAdd();
        modelBuilder.Entity<IncomeCategory>().Property(t => t.id).ValueGeneratedOnAdd();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
