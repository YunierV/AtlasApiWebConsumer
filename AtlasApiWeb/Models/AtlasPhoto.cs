using System.ComponentModel.DataAnnotations;

namespace AtlasApiWeb.Models
{
    public class AtlasPhoto
    {

        //marcamos con palabra reservada como queremos que el entity tracker mapee cada uno de los atributos
        [Key] //primary key en base de datos
        public int Id { get; set; }
        [Required] //dato requerido (no puede estar nulo)
        public string Title { get; set; }
        public string Photographer { get; set; }
        [Required] //dato requerido (no puede estar nulo)
        public string ImageUrl { get; set; }
        //public byte[] ImageUrl { get; set; } //si quisieramos cargarlas desde el local con un mapa de bytes


    }
}
