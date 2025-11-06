using System.ComponentModel.DataAnnotations;

namespace RecyRoute.NewFolder
{
    public class Rol
    {
        [Key]//se define la llave primaria
        public Guid IdRol { get; set; } = Guid.NewGuid();
        //aaa

        [Required]
        public string NombreRol { get; set; } = string.Empty;

        [Required]
        public string DescripcionRol { get; set; } = string.Empty;

        public virtual ICollection<Usuario>? Usuario { get; set; }
    }
}
