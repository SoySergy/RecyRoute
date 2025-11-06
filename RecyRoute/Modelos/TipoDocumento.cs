using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

RamaSergio
namespace RecyRoute.Modelos

namespace Recy_Route.Modelos
main
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
