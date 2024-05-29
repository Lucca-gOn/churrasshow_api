using System.Text.Json.Serialization;

namespace apiweb.churras.show.Dto
{
    public record ListarEventoResponse([property: JsonPropertyName("items")] IReadOnlyCollection<ListarEventosResponseItem> Itens);
}
