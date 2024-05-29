using apiweb.churras.show.Domains;

namespace apiweb.churras.show.Interfaces
{
    public interface IEnderecoRepository
    {
        void Cadastrar(Endereco novoEndereco);

        void Deletar(Guid id);

        List<Endereco> ListarTodos();

        void Atualizar(Guid Id, Endereco endereco);
    }
}
