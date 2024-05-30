using apiweb.churras.show.Domains;

namespace apiweb.churras.show.Interfaces
{
    public interface IComentarioRepository
    {
        void Cadastrar(Comentarios novoComentario);
        void Deletar(Guid id);
        List<Comentarios> Listar();
        void Atualizar(Guid id, Comentarios comentario);
        Comentarios BuscarPorId(Guid id);
        List<Comentarios> ListarSomenteExibe();
    }
}
