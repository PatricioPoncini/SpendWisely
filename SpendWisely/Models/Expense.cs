using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpendWisely.Models
{
    public class Expense
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre del gasto es obligatorio.")]
        public string title { get; set; }

        // el usuario elije si le agrega una descripción o no
        public string? description { get; set; }

        [DataType(DataType.Currency)]
        public decimal amount { get; set; }

        public int userId { get; set; }

        public int expenseCategoryId { get; set; }

        public DateTime registrationDate { get; set; }

        [ForeignKey("expenseCategoryId")]
        public ExpenseCategory expenseCategory { get; set; }

        [ForeignKey("userId")]
        public User user { get; set; }
    }
}
