using System.ComponentModel.DataAnnotations;

namespace RecyRoute.NewFolder
{
    public class Rol
    {
        [Key]//se define la llave primaria
        public Guid IdRol { get; set; } = Guid.NewGuid();
        
        //se definen los demas atributos de la clase
        public string NombreRol { get; set; }
        public string Descripcion { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
