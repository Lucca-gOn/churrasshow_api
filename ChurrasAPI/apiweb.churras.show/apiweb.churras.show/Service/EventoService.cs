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
        private readonly ChurrasShowContext _context;

        public EventoService(IPacoteRepository pacoteRepository, ChurrasShowContext context)
        {
            _pacoteRepository = pacoteRepository;
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
                                select new
                                {
                                    e.IdUsuario,
                                    u.Nome,
                                    e.DataHoraEvento,
                                    e.QuantidadePessoasEvento,
                                    e.DuracaoEvento,
                                    e.Descartaveis,
                                    e.Acompanhamentos,
                                    e.Garconete,
                                    e.Confirmado,
                                    p.IdPacotes,
                                    p.NomePacote,
                                    p.DescricaoPacote,
                                    p.ValorPorPessoa,
                                    e.IdEndereco,
                                    end.Logradouro,
                                    end.Cidade,
                                    end.UF,
                                    end.CEP,
                                    end.Numero,
                                    end.Bairro,
                                    end.Complemento,
                                    e.DataDeCriacao,
                                    s.Status,
                                    e.ValorTotal,
                                    MostrarValorTotal = e.IdUsuario == id
                                })
                                .AsEnumerable() // força a execução da consulta no banco de dados, lembrar result e AsEnumerable para metodos parecidos, muito dificil.
                                .Select(result => new ListarEventosResponseItem(
                                    result.IdUsuario,
                                    result.Nome,
                                    result.DataHoraEvento ?? DateTime.MinValue,
                                    result.QuantidadePessoasEvento ?? 0,
                                    result.DuracaoEvento ?? 0,
                                    result.Descartaveis ?? false,
                                    result.Acompanhamentos ?? false,
                                    result.Garconete ?? 0,
                                    result.Confirmado ?? false,
                                    result.IdPacotes,
                                    result.NomePacote,
                                    result.DescricaoPacote,
                                    result.ValorPorPessoa ?? 0m,
                                    result.IdEndereco,
                                    result.Logradouro,
                                    result.Cidade,
                                    result.UF,
                                    result.CEP ?? 0,
                                    result.Numero ?? 0,
                                    result.Bairro,
                                    result.Complemento,
                                    result.DataDeCriacao ?? DateTime.MinValue,
                                    result.Status,
                                    result.MostrarValorTotal ? result.ValorTotal ?? 0m : 0m 
                                ))
                                .ToList();

            return listaEventos;
        }

    }
}
