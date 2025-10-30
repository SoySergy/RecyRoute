using RecyRoute.NewFolder;
using System.ComponentModel.DataAnnotations;

namespace RecyRoute.Modelos
{
    public class TipoDoc
    {
        [Key]
        public Guid IdTipoDoc { get; set; } = Guid.NewGuid();
        public string NombreTipoDoc { get; set; }
        public string Abreviatura { get; set; }
        public string Descripcion { get; set; }
        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
