using apiweb.churras.show.Context;
using apiweb.churras.show.Domains;
using apiweb.churras.show.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace apiweb.churras.show.Repositories
{
    public class TiposUsuarioRepository : ITiposUsuarioRepository
    {
        private readonly ChurrasShowContext _context;

        public TiposUsuarioRepository(ChurrasShowContext context)
        {
            _context = context;
        }
        public List<TiposUsuario> ListarTodos()
        {
            try
            {
                return _context.TiposUsuario.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
