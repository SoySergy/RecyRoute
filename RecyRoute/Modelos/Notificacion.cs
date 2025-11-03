using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecyRoute.Modelos
{
    public class Notificacion
    {
        [Key]
        public Guid IdNotificacion { get; set; } = Guid.NewGuid();
        [ForeignKey("Usuario")]
        public Guid IdUsuario { get; set; }
        [ForeignKey("SolicitudRecoleccion")]
        public Guid IdSolicitud { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaCreacion { get; set; }


    }
}
