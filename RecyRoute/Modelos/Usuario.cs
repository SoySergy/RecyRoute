using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecyRoute.Modelos
{
    public class Usuario
    {
        [Key]
        public Guid IdUsuario { get; set; } = Guid.NewGuid();

        [Required]
        public Guid IdRol { get; set; }

        [Required]
        public Guid IdTipoDocumento { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public string Contrasena { get; set; } = string.Empty!;

        [Required]
        public string NumeroDeTelefono { get; set; } = string.Empty;

        [Required]
        public string Direccion { get; set; } = string.Empty;

        [Required]
        public DateTime FechaRegistro  { get; set; } = DateTime.Now;

        [JsonIgnore]
        public virtual Rol? Rol { get; set; }
        public virtual TipoDocumento? TipoDocumento { get; set; }
    }
}
