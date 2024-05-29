using apiweb.churras.show.Domains;

namespace apiweb.churras.show.Interfaces
{
    public interface IEventoRepository
    {
        void Cadastrar(Evento novoEvento);

        List<Evento> ListarTodos();
    }
}
