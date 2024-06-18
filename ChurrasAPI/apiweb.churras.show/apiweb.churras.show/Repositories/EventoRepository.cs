using apiweb.churras.show.Context;
using apiweb.churras.show.Domains;
using apiweb.churras.show.Interfaces;
using apiweb.churras.show.ViewModels;
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

        public void AtualizarStatus(Guid id, AtualizarStatusEventoViewModel model)
        {
            try
            {
                Evento eventoExistente = _context.Evento.FirstOrDefault(u => u.IdEvento == id)!;

                if (eventoExistente != null)
                {
                    eventoExistente.IdStatusEvento = model.IdStatusEvento;
                    _context.Update(eventoExistente);

                    Guid idAprovado = new Guid("787fd592-c85c-4049-ba5d-7a28f15795e1"); 
                    Guid idCancelado = new Guid("3786ca9b-8a94-4f1a-8a3e-0154dcf9a798");  

                    if (eventoExistente.IdStatusEvento == idAprovado)
                    {
                        var eventosMesmaData = _context.Evento
                            .Where(e => e.DataHoraEvento == eventoExistente.DataHoraEvento && e.IdEvento != id)
                            .ToList();

                        foreach (var evento in eventosMesmaData)
                        {
                            evento.IdStatusEvento = idCancelado;
                            _context.Update(evento);
                        }
                    }

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar status do evento", ex);
            }
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
