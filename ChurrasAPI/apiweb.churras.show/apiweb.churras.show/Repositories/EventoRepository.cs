using apiweb.churras.show.Context;
using apiweb.churras.show.Domains;
using apiweb.churras.show.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace apiweb.churras.show.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly ChurrasShowContext _context;

        public EventoRepository(ChurrasShowContext churrasShowContext)
        {
            _context = churrasShowContext;
        }
        public void Cadastrar(Evento novoEvento)
        {
            try
            {
                _context.Add(novoEvento);
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Evento>> EventoStatusAsync(string status)
        {
            try
            {
                var eventos = await _context.Evento
                    .Include(e => e.Pacotes)    
                    .Include(e => e.Endereco)    
                    .Include(e => e.Usuario)    
                    .Include(e => e.StatusEvento) 
                    .Where(e => e.StatusEvento.Status == status)
                    .ToListAsync();

                return eventos;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Evento> ListarTodos()
        {
            try
            {
                return _context.Evento.Include(e => e.Pacotes)
                               .Include(e => e.Endereco)
                               .Include(e => e.Usuario)
                               .ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
