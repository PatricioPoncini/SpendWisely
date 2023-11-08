using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpendWisely.Models
{
    public class Income
    {
        [Required(ErrorMessage = "El título del gasto es obligatorio.")]
        public string title { get; set; }

        public string description { get; set; }

        public DateTime registrationDate { get; set; }

        [Required(ErrorMessage = "El monto del ingreso es obligatorio.")]
        [DataType(DataType.Currency)]
        public decimal amount { get; set; }

        public int userId { get; set; }

        public int incomeCategoryId { get; set; }

        [ForeignKey("userId")]
        public User user { get; set; }

        [ForeignKey("incomeCategoryId")]
        public IncomeCategory incomeCategory { get; set; }
    }
}
