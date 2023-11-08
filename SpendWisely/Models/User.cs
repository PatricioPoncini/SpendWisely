using System;
using System.ComponentModel.DataAnnotations;

namespace SpendWisely.Models
{
    public class User
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre del usuario es obligatorio.")]
        public string firstname { get; set; }

        [Required(ErrorMessage = "El apellido de usuario es obligatorio.")]
        public string lastname { get; set; }

        [Required(ErrorMessage = "El nickname de usuario es obligatorio.")]
        public string nickname { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        public DateTime registrationDate { get; set; }
    }
}
