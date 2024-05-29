using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace apiweb.churras.show.ViewModels
{
    public class UsuarioViewModel
    {
        [NotMapped]
        [JsonIgnore]
        public IFormFile? Arquivo { get; set; }
        public string? Foto { get; set; }
    }
}
