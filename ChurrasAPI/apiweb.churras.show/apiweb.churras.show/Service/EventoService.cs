using apiweb.churras.show.Context;
using apiweb.churras.show.Domains;
using apiweb.churras.show.Dto;
using apiweb.churras.show.Interfaces;
using apiweb.churras.show.Repositories;
using apiweb.churras.show.Utils;
using Microsoft.EntityFrameworkCore;

namespace apiweb.churras.show.Service
{
    public class EventoService : IEventoService
    {
        private readonly IPacoteRepository _pacoteRepository;
        private readonly IEventoService _eventoService;
        private readonly IEventoRepository _eventoRepository;
        private readonly ChurrasShowContext _context;

        public EventoService(IPacoteRepository pacoteRepository, IEventoService eventoService, IEventoRepository eventoRepository, ChurrasShowContext context)
        {
            _pacoteRepository = pacoteRepository;
            _eventoService = eventoService;
            _eventoRepository = eventoRepository;
            _context = context;
        }

        public async Task<decimal> CalcularValorTotal(Evento evento)
        {
            // Obter o pacote correspondente
            var pacote = await _pacoteRepository.BuscarPorId(evento.IdPacotes);
            evento.Pacotes = pacote;

            // Valor base (valor por pessoa multiplicado pela quantidade de pessoas)
            decimal totalValue = (decimal)(evento.Pacotes.ValorPorPessoa * evento.QuantidadePessoasEvento.Value)!;

            // Adicionar valor adicional se a duração do evento for maior que 4 horas
            if (evento.DuracaoEvento > 4)
            {
                totalValue += (evento.DuracaoEvento.Value - 4) * EventoConstantes.ValorHoraAdicional;
            }

            // Adicionar valor de descartáveis
            if (evento.Descartaveis == true)
            {
                totalValue += evento.QuantidadePessoasEvento.Value * EventoConstantes.ValorPorPessoaDescartavel;
            }

            // Adicionar valor de acompanhamentos
            if (evento.Acompanhamentos == true)
            {
                totalValue += evento.QuantidadePessoasEvento.Value * EventoConstantes.ValorPorPessoaAcompanhamentos;
            }

            // Adicionar valor de garçom
            if (evento.Garconete.HasValue)
            {
                totalValue += evento.Garconete.Value * EventoConstantes.ValorGarcom;
            }

            return totalValue;
        }

        public List<ListarEventosResponseItem> ListarEventosValorTotal(Guid id)
        {
            var listaEventos = (from e in _context.Evento
                                join p in _context.Pacote on e.IdPacotes equals p.IdPacotes
                                join end in _context.Endereco on e.IdEndereco equals end.IdEndereco
                                join u in _context.Usuario on e.IdUsuario equals u.IdUsuario
                                join s in _context.StatusEvento on e.IdStatusEvento equals s.IdStatusEvento
                                select new ListarEventosResponseItem(
                                    u.Nome,
                                    e.DataHoraEvento ?? DateTime.MinValue,
                                    e.QuantidadePessoasEvento ?? 0,
                                    e.DuracaoEvento ?? 0,
                                    e.Descartaveis ?? false,
                                    e.Acompanhamentos ?? false,
                                    e.Garconete ?? 0,
                                    e.Confirmado ?? false,
                                    e.IdPacotes,
                                    p.NomePacote,
                                    p.DescricaoPacote,
                                    p.ValorPorPessoa ?? 0,
                                    e.IdEndereco,
                                    end.Logradouro,
                                    end.Cidade,
                                    end.UF,
                                    end.CEP ?? 0,
                                    end.Numero ?? 0,
                                    end.Bairro,
                                    end.Complemento,
                                    e.DataDeCriacao ?? DateTime.MinValue,
                                    s.Status,
                                    e.IdUsuario == id ? e.ValorTotal : (decimal?)null // Oculta o valor total se não for do usuário logado
                                )).ToList().AsReadOnly();

            return listaEventos;
        }
    }
}
