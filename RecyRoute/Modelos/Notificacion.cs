using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecyRoute.Modelos
{
    public class Notificacion
    {
        [Key]
        public Guid IdNotificacion { get; set; } = Guid.NewGuid();

        [Required]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public string Mensaje { get; set; } = string.Empty;

        [Required]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [Required]
        public bool Leida { get; set; } = false;

        // Llaves Foraneas
        public Guid? IdUsuario { get; set; }
        public Guid? IdSolicitud { get; set; }

        // Propiedades de navegacion
        [JsonIgnore]
        public virtual Usuario? Usuario { get; set; }

        [JsonIgnore]
        public virtual SolicitudRecoleccion? SolicitudRecoleccion { get; set; }
    }
}
