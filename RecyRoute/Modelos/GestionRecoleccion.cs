using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecyRoute.Modelos
{
    public class GestionRecoleccion
    {
        [Key]
        public Guid IdGestion { get; set; } = Guid.NewGuid();

        [Required]
        public Guid IdSolicitud { get; set; }

        [Required]
        public Guid IdGestor { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = string.Empty;

        public DateTime? FechaCambioEstado { get; set; }

        public DateTime? FechaProgramada { get; set; }

        public DateTime? FechaRealizacion { get; set; }

        [StringLength(200)]
        public string? ObservacionesGestor { get; set; }

        // Propiedades de navegación
        [JsonIgnore]
        public virtual SolicitudRecoleccion? SolicitudRecoleccion { get; set; }

        [JsonIgnore]
        public virtual Usuario? Gestor { get; set; }
    }
}
