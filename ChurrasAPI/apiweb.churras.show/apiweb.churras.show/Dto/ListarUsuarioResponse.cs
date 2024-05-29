using System.Text.Json.Serialization;

namespace apiweb.churras.show.Dto
{
    public record ListarUsuariosResponse([property: JsonPropertyName("items")] IReadOnlyCollection<ListarUsuariosResponseItem> Itens);
}
