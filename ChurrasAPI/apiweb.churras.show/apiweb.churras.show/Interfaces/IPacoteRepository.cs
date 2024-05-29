using apiweb.churras.show.Domains;
using Org.BouncyCastle.Bcpg;

namespace apiweb.churras.show.Interfaces
{
    public interface IPacoteRepository
    {
        void Cadastrar(Pacotes novoPacote);

        List<Pacotes> ListarTodos();

        Task<Pacotes> BuscarPorId(Guid id);
    }
}
