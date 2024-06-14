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

        public void AtualizarUsuario(Guid id, AtualizarUsuarioViewModel model);

        bool AlterarSenha(string email, string senhaNova);

        public void AtualizarFoto(Guid id, string novaUrlFoto);
    }
}
