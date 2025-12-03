using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecyRoute.Modelos
{
    public class SolicitudRecoleccion
    {
        [Key]
        public Guid IdSolicitud { get; set; } = Guid.NewGuid();

        [Required]
        public Guid IdUsuario { get; set; }

        [Required]
        public DateTime FechaDeRecoleccion { get; set; }

        [Required]
        [StringLength(20)]
        public string HoraDeRecoleccion { get; set;  } = string.Empty;

        [Required]
        [StringLength(200)]
        public string DireccionRecoleccion { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string TelefonoContacto { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string EstadoActual { get; set; } = "Pendiente";

        // Fecha de la creacion de la solicitud
        [Required]
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;

        [Required]
        [StringLength(200)]
        public string TipoResiduos { get; set; } = string.Empty;

        [StringLength(500)]
        public string? ObservacionesCiudadano { get; set; }

        [JsonIgnore]
        public virtual Usuario? Usuario { get; set; }

        [JsonIgnore]
        public virtual ICollection<Historial>? Historial { get; set; }
    }
}
