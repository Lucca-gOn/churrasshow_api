using apiweb.churras.show.Context;
using apiweb.churras.show.Interfaces;

namespace apiweb.churras.show
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChurrasShowContext _context;

        public UnitOfWork(ChurrasShowContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
