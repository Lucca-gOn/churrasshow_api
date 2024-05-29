using apiweb.churras.show.Context;
using apiweb.churras.show.Domains;
using apiweb.churras.show.Interfaces;

namespace apiweb.churras.show.Repositories
{
    public class StatusEventoRepository : IStatusEventoRepository
    {
        private readonly ChurrasShowContext _context;

        public StatusEventoRepository(ChurrasShowContext context)
        {
            _context = context;
        }
        public List<StatusEvento> ListarTodos()
        {
            try
            {
                return _context.StatusEvento.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
