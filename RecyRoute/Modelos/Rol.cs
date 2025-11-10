using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecyRoute.Modelos
{
    public class Rol 
    {
        [Key]
        public Guid IdRol { get; set; } = Guid.NewGuid();

        [Required]
        public string NombreRol { get; set; } = string.Empty;

        [Required]
        public string DescripcionRol { get; set; } = string.Empty;

        public virtual ICollection<Usuario>? Usuario { get; set; }
    }
}
