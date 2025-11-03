using System.ComponentModel.DataAnnotations;

namespace RecyRoute.NewFolder
{
    public class Rol
    {
        [Key]
        public Guid IdRol { get; set; } = Guid.NewGuid();
        public string NombreRol { get; set; }
        public string Descripcion { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
