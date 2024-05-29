using apiweb.churras.show.Domains;
using apiweb.churras.show.Dto;

namespace apiweb.churras.show.Interfaces
{
    public interface IEventoService
    {
        Task<decimal> CalcularValorTotal(Evento evento);
        List<ListarEventosResponseItem> ListarEventosValorTotal(Guid id);

    }
}
