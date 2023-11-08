using System.ComponentModel.DataAnnotations;

namespace SpendWisely.Models
{
    public class IncomeCategory
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio al crear una categoría de ingreso")]
        public string title { get; set; }

        [Required(ErrorMessage = "El ícono es obligatorio al crear una categoría de ingreso")]
        public string icon { get; set; }
    }
}
