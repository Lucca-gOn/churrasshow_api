using apiweb.churras.show.Domains;
using apiweb.churras.show.ViewModels;

namespace apiweb.churras.show.Interfaces
{
    public interface IUsuarioRepository
    {
        void Cadastrar(Usuario novoUsuario);

        Usuario BuscarPorId(Guid id);

        Usuario BuscarPorEmailESenha(string email, string senha);

        void Deletar(Guid idUsuario);

        void Atualizar(Guid id, AtualizarUsuarioViewModel dadosAtualizados);

        bool AlterarSenha(string email, string senhaNova);
    }
}
