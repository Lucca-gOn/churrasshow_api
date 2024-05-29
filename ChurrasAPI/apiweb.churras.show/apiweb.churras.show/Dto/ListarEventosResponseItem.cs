using System.Text.Json.Serialization;

namespace apiweb.churras.show.Dto
{
    public record ListarEventosResponseItem(
    [property: JsonPropertyName("_id")] Guid IdUsuario,
    [property: JsonPropertyName("nomeCliente")] string Nome,
    [property: JsonPropertyName("dataHoraEvento")] DateTime DataHoraEvento,
    [property: JsonPropertyName("quantidadePessoasEvento")] int QuantidadePessoasEvento,
    [property: JsonPropertyName("duracaoEvento")] int DuracaoEvento,
    [property: JsonPropertyName("descartaveis")] bool Descartaveis,
    [property: JsonPropertyName("acompanhamentos")] bool Acompanhamentos,
    [property: JsonPropertyName("garconete")] int Garconete,
    [property: JsonPropertyName("confirmado")] bool Confirmado,
    [property: JsonPropertyName("idPacote")] Guid IdPacote,
    [property: JsonPropertyName("nomePacote")] string NomePacote,
    [property: JsonPropertyName("descricaoPacote")] string DescricaoPacote,
    [property: JsonPropertyName("valorPorPessoa")] decimal ValorPorPessoa,
    [property: JsonPropertyName("idEndereco")] Guid IdEndereco,
    [property: JsonPropertyName("logradouro")] string Logradouro,
    [property: JsonPropertyName("cidade")] string Cidade,
    [property: JsonPropertyName("uf")] string UF,
    [property: JsonPropertyName("cep")] int CEP,
    [property: JsonPropertyName("numero")] int Numero,
    [property: JsonPropertyName("bairro")] string Bairro,
    [property: JsonPropertyName("complemento")] string Complemento,
    [property: JsonPropertyName("dataDeCriacao")] DateTime DataDeCriacao,
    [property: JsonPropertyName("statusEvento")] string Status,
    [property: JsonPropertyName("valorTotal")] decimal ValorTotal);
}
