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
        [StringLength(200)]
        public string DireccionRecoleccion { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string EstadoActual { get; set; } = string.Empty;

        [Required]
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;

        [Required]
        [StringLength(100)]
        public string TipoResiduos { get; set; } = string.Empty;

        [StringLength(500)]
        public string? ObservacionesCiudadano { get; set; }

        [JsonIgnore]
        public virtual Usuario? Usuario { get; set; }
    }
}
