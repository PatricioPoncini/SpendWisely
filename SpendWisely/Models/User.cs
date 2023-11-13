using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpendWisely.Models
{
    public class User
    {
        [Key]
        [Column("ID")]
        public int id { get; set; }

        [Display(Name ="Nombre")]
        [Required(ErrorMessage = "El nombre del usuario es obligatorio.")]
        public string firstname { get; set; }

        [Display(Name ="Apellido")]
        [Required(ErrorMessage = "El apellido de usuario es obligatorio.")]
        public string lastname { get; set; }

        [Display(Name ="Nombre de usuario")]
        [Required(ErrorMessage = "El nickname de usuario es obligatorio.")]
        public string nickname { get; set; }

        [Display(Name ="Contraseña")]
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name ="Email")]
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Display(Name = "Fecha de registro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime registrationDate { get; set; }
    }

    public class UserDBContext : DbContext
    {
        public UserDBContext() { }

        public DbSet<User> Users { get; set; }
    }
}
