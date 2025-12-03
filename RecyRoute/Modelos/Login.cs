using System.ComponentModel.DataAnnotations;

namespace RecyRoute.Modelos
{
    public class Login 
    {
        [Required]
        // Incorrecto: public string NombreUsuario { get; set; } = string.Empty!;

        public string Correo { get; set; } = string.Empty!;
        [Required]
        public string Contrasena { get; set; } = string.Empty!;
    } 
}
