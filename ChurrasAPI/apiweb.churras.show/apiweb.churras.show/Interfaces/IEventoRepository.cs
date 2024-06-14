using apiweb.churras.show.Domains;
using apiweb.churras.show.ViewModels;

namespace apiweb.churras.show.Interfaces
{
    public interface IEventoRepository
    {
        void Cadastrar(Evento novoEvento);

        List<Evento> ListarTodos();

        Task<List<Evento>> EventoStatusAsync(string status);

        void AtualizarStatus(Guid id, AtualizarStatusEventoViewModel model);

    }
}
