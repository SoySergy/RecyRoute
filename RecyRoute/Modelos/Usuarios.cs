using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecyRoute.NewFolder
{
    public class Usuarios:Rol
    {
        [Key]
        public Guid IdUsuario { get; set; }
        = Guid.NewGuid();
        [ForeignKey("Roles")]
        public Guid IdRol { get; set; }
        [ForeignKey("TiposDocumentos")]
        public Guid IdTipoDoc { get; set; }

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
