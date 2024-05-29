using System.Text.Json.Serialization;

namespace apiweb.churras.show.Dto
{
    public record ListarUsuariosResponseItem(
     [property: JsonPropertyName("_id")] Guid Id,
     [property: JsonPropertyName("_idEndereco")] Guid IdEndereco,
     [property: JsonPropertyName("name")] string Nome,
     [property: JsonPropertyName("email")] string Email,
     [property: JsonPropertyName("rg")] string RG,
     [property: JsonPropertyName("cpf")] string CPF,
     [property: JsonPropertyName("image_src")] string Foto,
     [property: JsonPropertyName("userType")] string NomeType,
     [property: JsonPropertyName("logradouro")] string Logradouro,
     [property: JsonPropertyName("cidade")] string Cidade,
     [property: JsonPropertyName("uf")] string UF,
     [property: JsonPropertyName("cep")] string CEP,
     [property: JsonPropertyName("number")] string numero,
     [property: JsonPropertyName("bairro")] string bairro,
     [property: JsonPropertyName("complemento")] string complemento);
}
