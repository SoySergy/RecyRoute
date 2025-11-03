using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecyRoute.Modelos
{
    public class GestionRecolecion
    {
        [Key]
        public Guid IdGestion { get; set; } = Guid.NewGuid();
        [ForeignKey("SolicitudRecoleccion")]
        public Guid IdSolicitud { get; set; }

        public string Estado { get; set; }
        public DateTime FechaCambioEstado { get; set; }
        public DateTime FechaProgramada {  get; set; }
        public DateTime FechaRealizacion { get; set; }
        public string ObservacionGestor { get; set; }

    }
}
