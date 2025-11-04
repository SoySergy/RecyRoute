using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecyRoute.Modelos
{
    public class LogGeneral
    {
        [Key] //se define la llave rimaria
        public Guid IdLog { get; set; }= Guid.NewGuid();

        //se definen los atributos restantes de la clase
        public string TipoLog { get; set; }
        public string Usuario {  get; set; }
        public DateTime Fecha { get; set; }
        public string ValoresViejo { get; set; }
        public string ValoresNuevo { get; set; }
        public string MensajeErro { get; set; }
        public string StackTrace { get; set; }
        public string Severidad { get; set; }
        public string Modelo { get; set; }
        public string DatosContexto { get; set; }
        public string TipoEvento { get; set; }
        public string Descripcion { get; set; }
        public bool Exitoso { get; set; }

    }
}
