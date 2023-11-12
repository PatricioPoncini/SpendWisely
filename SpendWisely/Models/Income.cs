using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpendWisely.Models
{
    public class Income
    {
        [Key]
        [Column("ID")]
        public int id { get; set; }

        [Display(Name ="Título")]
        [Required(ErrorMessage = "El título del gasto es obligatorio.")]
        public string title { get; set; }

        [Display(Name ="Descripción")]
        public string? description { get; set; }

        [Display(Name = "Fecha de registro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime registrationDate { get; set; }

        [Required(ErrorMessage = "El monto del ingreso es obligatorio.")]
        [Display(Name ="Monto")]
        [DataType(DataType.Currency)]
        public decimal amount { get; set; }

        public int userId { get; set; }

        public int incomeCategoryId { get; set; }

        [ForeignKey("userId")]
        public User user { get; set; }

        [Display(Name ="Categoría")]
        [ForeignKey("incomeCategoryId")]
        public IncomeCategory incomeCategory { get; set; }
    }
}
