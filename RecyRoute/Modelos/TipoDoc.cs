using RecyRoute.NewFolder;
using System.ComponentModel.DataAnnotations;

namespace RecyRoute.Modelos
{
    public class TipoDoc
    {
        [Key] //Se define la llave primaria
        public Guid IdTipoDoc { get; set; } = Guid.NewGuid();
        
        //se define los atrbutos restantes de la clase
        public string NombreDoc { get; set; }
        public string Abreviatura { get; set; }        
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
