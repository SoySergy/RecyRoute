using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecyRoute.NewFolder
{
    public class Usuario:Rol
    {
        [Key] //se define la llave primaria
        public Guid IdUsuario { get; set; }
        = Guid.NewGuid();

        //se definen las llaves foraneas
        [ForeignKey("Roles")]
        public Guid IdRol { get; set; }
        [ForeignKey("TiposDocumentos")]
        public Guid IdTipoDoc { get; set; }

        //se definen los atributos restantes de la clase 
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
    }
}
