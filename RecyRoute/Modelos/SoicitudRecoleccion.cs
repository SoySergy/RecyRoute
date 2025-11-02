using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecyRoute.Modelos
{
    public class SoicitudRecoleccion
    {
        [Key]
        public Guid IdSolicitud { get; set; } = Guid.NewGuid();
        [ForeignKey("Usuarios")]
        public Guid IdUsuario { get; set; }
        public string DireccionRecoleccion { get; set; }
        public string EstadoActual{ get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string TipoResiduo { get; set; }
        public string ObservacionesCiudadano { get; set; }



    }
}
