using apiweb.churras.show.Context;
using apiweb.churras.show.Domains;
using apiweb.churras.show.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace apiweb.churras.show.Repositories
{
    public class ComentarioRepository : IComentarioRepository
    {
        private readonly ChurrasShowContext _context;

        public ComentarioRepository(ChurrasShowContext context)
        {
            _context = context;
        }

        public void Atualizar(Guid id, Comentarios comentario)
        {
            try
            {
                Comentarios buscarComentario = _context.Comentario.Find(id)!;
                if (buscarComentario != null)
                {
                    buscarComentario.DescricaoComentario = comentario.DescricaoComentario;
                    buscarComentario.Pontuacao = comentario.Pontuacao;
                    buscarComentario.Exibe = comentario.Exibe;
                    

                    _context.Update(buscarComentario);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Comentarios BuscarPorId(Guid id)
        {
            try
            {
                return _context.Comentario.FirstOrDefault(e => e.IdComentario == id)!;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Cadastrar(Comentarios novoComentario)
        {
            try
            {
                _context.Comentario.Add(novoComentario);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Deletar(Guid id)
        {
            try
            {
                _context.Comentario.Where(e => e.IdComentario == id).ExecuteDelete();
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Comentarios> Listar()
        {
            try
            {
                return _context.Comentario.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Comentarios> ListarSomenteExibe()
        {
            try
            {
                return _context.Comentario
                    .Include(c => c.Usuario)
                    .Where(c => c.Exibe)
                    .Select(c => new Comentarios
                    {
                        IdComentario = c.IdComentario,
                        DescricaoComentario = c.DescricaoComentario,
                        Pontuacao = c.Pontuacao,
                        Exibe = c.Exibe,
                        IdEvento = c.IdEvento,
                        IdUsuario = c.IdUsuario,
                        Usuario = new Usuario
                        {
                            Nome = c.Usuario!.Nome,
                            Foto = c.Usuario.Foto,
                        }
                    })
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
