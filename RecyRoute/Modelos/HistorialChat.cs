using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecyRoute.Modelos
{
    public class HistorialChat
    {
        [Key]
        public Guid IdHistorialChat { get; set; } = Guid.NewGuid();

        [Required]
        public Guid IdSolicitud { get; set; }

        [Required]
        public Guid IdEmisor { get; set; }

        [Required]
        public string Mensaje { get; set; } = string.Empty;

        [Required]
        public DateTime FechaEnvio { get; set; } = DateTime.Now;

        [Required]
        public bool Leido { get; set; } = false;

        // Relaciones de navegación
        [JsonIgnore]
        public virtual SolicitudRecoleccion? SolicitudRecoleccion { get; set; }

        [JsonIgnore]
        public virtual Usuario? Emisor { get; set; }
    }
}

