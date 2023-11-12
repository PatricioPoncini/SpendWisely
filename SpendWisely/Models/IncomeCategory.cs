using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpendWisely.Models
{
    public class IncomeCategory
    {
        [Key]
        [Column("ID")]
        public int id { get; set; }

        [Display(Name ="Título")]
        [Required(ErrorMessage = "El título es obligatorio al crear una categoría de ingreso")]
        public string title { get; set; }

        [Display(Name ="Ícono")]
        [Required(ErrorMessage = "El ícono es obligatorio al crear una categoría de ingreso")]
        public string icon { get; set; }
    }
}
