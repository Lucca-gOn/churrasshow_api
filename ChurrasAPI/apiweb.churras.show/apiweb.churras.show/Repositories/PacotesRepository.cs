using apiweb.churras.show.Context;
using apiweb.churras.show.Domains;
using apiweb.churras.show.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace apiweb.churras.show.Repositories
{
    public class PacotesRepository : IPacoteRepository
    {
        private readonly ChurrasShowContext _context;

        public PacotesRepository(ChurrasShowContext context)
        {
            _context = context;
        }

        public void Cadastrar(Pacotes novoPacote)
        {
            try
            {
                _context.Add(novoPacote);
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Pacotes> ListarTodos()
        {
            try
            {
                return _context.Pacote.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Pacotes> BuscarPorId(Guid id)
        {
            try
            {
                return await _context.Pacote.FirstOrDefaultAsync(e => e.IdPacotes == id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
