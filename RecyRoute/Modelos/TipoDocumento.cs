using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecyRoute.Modelos
{
    public class TipoDocumento
    {
        [Key]
        public Guid IdTipoDocumento { get; set; } = Guid.NewGuid();

        [Required]
        public string NombreDocumento { get; set; } = string.Empty;

        [Required]
        public string Abreviatura { get; set; } = string.Empty;

        [JsonIgnore]
        public virtual ICollection<Usuario>? Usuario { get; set; }
    }
}
