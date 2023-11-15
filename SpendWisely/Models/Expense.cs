using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpendWisely.Models
{
    public class Expense
    {
        [Key]
        [Column("ID")]
        public int id { get; set; }

        [Display(Name ="Título")]
        [Required(ErrorMessage = "El nombre del gasto es obligatorio.")]
        public string title { get; set; }

        [Display(Name = "Descripción")]
        public string? description { get; set; }

        [Required(ErrorMessage = "El monto del gasto es obligatorio.")]
        [Display(Name ="Monto")]
        [DataType(DataType.Currency)]
        public int amount { get; set; }

        public int expenseCategoryId { get; set; }

        [Display(Name ="Fecha de registro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime registrationDate { get; set; }

        [Display(Name ="Categoría")]
        [ForeignKey("expenseCategoryId")]
        public ExpenseCategory expenseCategory { get; set; }
    }
}
