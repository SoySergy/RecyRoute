using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecyRoute.Modelos
{
    public class Historial
    {
        [Key]
        public Guid IdHistorial { get; set; } = Guid.NewGuid();

        [Required]
        public Guid IdSolicitud { get; set; }

        [Required]
        public Guid IdUsuario { get; set; }

        [StringLength(20)]
        public string? EstadoAnterior { get; set; }

        [Required]
        [StringLength(20)]
        public string EstadoNuevo { get; set; } = string.Empty;

        [Required]
        public DateTime FechaCambio { get; set; } = DateTime.Now;

        [StringLength(500)]
        public string? Comentario { get; set; }

        [JsonIgnore]
        public virtual SolicitudRecoleccion? SolicitudRecoleccion { get; set; }

        [JsonIgnore]
        public virtual Usuario? Usuario { get; set; }
    }
}
