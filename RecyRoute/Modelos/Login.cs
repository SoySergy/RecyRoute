using System.ComponentModel.DataAnnotations;

namespace RecyRoute.Modelos
{
    public class Login
    {
        [Required]
        public string NombreUsuario { get; set; } = string.Empty!;
        [Required]
        public Stream Contraseña { get; set; }
    } 
}
