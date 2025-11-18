using System.ComponentModel.DataAnnotations;

namespace RecyRoute.Modelos
{
    public class Login 
    {
        [Required]
        public string NombreUsuario { get; set; } = string.Empty!;
<<<<<<< HEAD
        [Required]
        public string Contrasena { get; set; } = string.Empty!;

    }
=======
        //[Required]
        //public Stream Contraseña { get; set; } = string.Empty!;
    } 
>>>>>>> 091ff259e12665db99cb0db055c02f4f89dbf9fa
}
