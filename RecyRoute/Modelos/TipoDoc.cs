using RecyRoute.NewFolder;
using System.ComponentModel.DataAnnotations;

namespace RecyRoute.Modelos
{
    public class TipoDoc
    {
        [Key]
        public Guid IdTipoDoc { get; set; } = Guid.NewGuid();
        public string NombreDoc { get; set; }
        public string Abreviatura { get; set; }        
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
