using apiweb.churras.show.Context;
using apiweb.churras.show.Domains;
using apiweb.churras.show.Interfaces;
using apiweb.churras.show.ViewModels;

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
