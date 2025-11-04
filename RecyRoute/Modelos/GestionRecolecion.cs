using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecyRoute.Modelos
{
    public class GestionRecolecion
    {
        [Key] //se Define llave primaria
        public Guid IdGestion { get; set; } = Guid.NewGuid();
        
        //se define llave foranea
        [ForeignKey("SolicitudRecoleccion")]
        public Guid IdSolicitud { get; set; }

        // se definen los atributos restantes de la clase
        public string Estado { get; set; }
        public DateTime FechaCambioEstado { get; set; }
        public DateTime FechaProgramada {  get; set; }
        public DateTime FechaRealizacion { get; set; }
        public string ObservacionGestor { get; set; }

    }
}
