using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace apiweb.churras.show.ViewModels
{
    public class CriarUsuarioClienteViewModel
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Rg { get; set; }
        public string? Cpf { get; set; }
        public string? Logradouro { get; set; }
        public string? Cidade { get; set; }
        public string? Uf { get; set; }
        public int? Cep { get; set; }
        public int? Numero { get; set; }
        public string? Bairro { get; set; }
        public string? Complemento { get; set; }
        public Guid IdTipoUsuario { get; set; }
        public string? Foto { get; set; }

        [JsonIgnore]
        [NotMapped]
        public IFormFile? Arquivo { get; set; }
    }
}
