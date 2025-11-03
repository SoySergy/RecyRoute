using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecyRoute.Modelos
{
    public class SoicitudRecoleccion
    {
        [Key]//se define la llave primaria
        public Guid IdSolicitud { get; set; } = Guid.NewGuid();
        
        //se define la llave foranea
        [ForeignKey("Usuarios")]
        public Guid IdUsuario { get; set; }
        
        //se definen los atributos restantes de la clase
        public string DireccionRecoleccion { get; set; }
        public string EstadoActual{ get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string TipoResiduo { get; set; }
        public string ObservacionesCiudadano { get; set; }



    }
}
