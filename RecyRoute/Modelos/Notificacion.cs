using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecyRoute.Modelos
{
    public class Notificacion
    {
        [Key] // se define IdNotificacion como llave primaria
        public Guid IdNotificacion { get; set; } = Guid.NewGuid();
        
        //Se definen llaves foraneas
        [ForeignKey("Usuario")]
        public Guid IdUsuario { get; set; }
        [ForeignKey("SolicitudRecoleccion")]
        public Guid IdSolicitud { get; set; }

        //Se definen los atrubutos reatantes de la clase
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaCreacion { get; set; }


    }
}
