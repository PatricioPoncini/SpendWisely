using System.ComponentModel.DataAnnotations;

namespace SpendWisely.Models
{
    public class ExpenseCategory
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio al crear una categoría de gasto")]
        public string title {  get; set; }

        [Required(ErrorMessage = "El ícono es obligatorio al crear una categoría de gasto")]
        public string icon { get; set; }
    }
}
