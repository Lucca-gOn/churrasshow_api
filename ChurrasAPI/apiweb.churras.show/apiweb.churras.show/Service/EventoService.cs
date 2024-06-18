using apiweb.churras.show.Context;
using apiweb.churras.show.Domains;
using apiweb.churras.show.Dto;
using apiweb.churras.show.Interfaces;
using apiweb.churras.show.Repositories;
using apiweb.churras.show.Utils;
using Microsoft.Azure.CognitiveServices.ContentModerator.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;

namespace apiweb.churras.show.Service
{
    public class EventoService : IEventoService
    {
        private readonly IPacoteRepository _pacoteRepository;
        private readonly ChurrasShowContext _context;
        private readonly IEventoRepository _eventoRepository;

        public EventoService(IPacoteRepository pacoteRepository, ChurrasShowContext context, IEventoRepository eventoRepository)
        {
            _pacoteRepository = pacoteRepository;
            _context = context;
            _eventoRepository = eventoRepository;
        }

        public List<ListarEventosResponseItem> BuscarPorData(DateTime dataEvento)
        {
            var PrioridadeStatus = new Dictionary<Guid, int>
            {
                { new Guid("787fd592-c85c-4049-ba5d-7a28f15795e1"), 1 },
                { new Guid("85ff68f5-ac18-4a5a-a500-374ce5bfb813"), 2 },
                { new Guid("3786ca9b-8a94-4f1a-8a3e-0154dcf9a798"), 3 }
            };
                                                                                                                                                                            
            var eventos = _context.Evento
                .Include(e => e.Pacotes)
                .Include(e => e.Endereco)
                .Include(e => e.Usuario)
                .Include(e => e.StatusEvento)
                .Where(x => x.DataHoraEvento.HasValue &&
                            x.DataHoraEvento.Value.Year == dataEvento.Year &&
                            x.DataHoraEvento.Value.Month == dataEvento.Month &&
                            x.DataHoraEvento.Value.Day == dataEvento.Day)
                .ToList();  
            eventos = eventos
                .OrderBy(x => PrioridadeStatus[x.StatusEvento.IdStatusEvento])
                .ToList();
            return eventos.Select(x => new ListarEventosResponseItem(
                x.IdEvento,
                x.IdUsuario,
                x.Usuario.Nome,
                x.Usuario.Foto,
                x.DataHoraEvento.Value,
                x.QuantidadePessoasEvento ?? 0,
                x.DuracaoEvento ?? 0,
                x.Descartaveis ?? false,
                x.Acompanhamentos ?? false,
                x.Garconete ?? 0,
                x.Confirmado ?? false,
                x.IdPacotes,
                x.Pacotes.NomePacote,
                x.Pacotes.DescricaoPacote,
                x.Pacotes.ValorPorPessoa ?? 0m,
                x.IdEndereco,
                x.Endereco.Logradouro,
                x.Endereco.Cidade,
                x.Endereco.UF,
                x.Endereco.CEP ?? 0,
                x.Endereco.Numero ?? 0,
                x.Endereco.Bairro,
                x.Endereco.Complemento,
                x.DataDeCriacao ?? DateTime.MinValue,
                x.StatusEvento.Status,
                x.ValorTotal ?? 0m
            )).ToList();
        }



        public async Task<List<ListarEventosResponseItem>> BuscarPorStatusAsync(string status)
        {
            try
            {
                var eventos = await _eventoRepository.EventoStatusAsync(status);

                var resultado = eventos.Select(x => new ListarEventosResponseItem(
                    IdEvento: x.IdEvento,
                    IdUsuario: x.IdUsuario,
                    Nome: x.Usuario.Nome,
                    Foto: x.Usuario.Foto,
                    DataHoraEvento: x.DataHoraEvento.Value,
                    QuantidadePessoasEvento: x.QuantidadePessoasEvento ?? 0,
                    DuracaoEvento: x.DuracaoEvento ?? 0,
                    Descartaveis: x.Descartaveis ?? false,
                    Acompanhamentos: x.Acompanhamentos ?? false,
                    Garconete: x.Garconete ?? 0,
                    Confirmado: x.Confirmado ?? false,
                    IdPacote: x.IdPacotes,
                    NomePacote: x.Pacotes.NomePacote,
                    DescricaoPacote: x.Pacotes.DescricaoPacote,
                    ValorPorPessoa: x.Pacotes.ValorPorPessoa ?? 0m,
                    IdEndereco: x.IdEndereco,
                    Logradouro: x.Endereco.Logradouro,
                    Cidade: x.Endereco.Cidade,
                    UF: x.Endereco.UF,
                    CEP: x.Endereco.CEP ?? 0,
                    Numero: x.Endereco.Numero ?? 0,
                    Bairro: x.Endereco.Bairro,
                    Complemento: x.Endereco.Complemento,
                    DataDeCriacao: x.DataDeCriacao ?? DateTime.MinValue,
                    Status: x.StatusEvento.Status,
                    ValorTotal: x.ValorTotal ?? 0m
                )).ToList();

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao buscar eventos pelo status.", ex);
            }
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
            try
            {
                var listaEventos = (from e in _context.Evento
                                    join p in _context.Pacote on e.IdPacotes equals p.IdPacotes
                                    join end in _context.Endereco on e.IdEndereco equals end.IdEndereco
                                    join u in _context.Usuario on e.IdUsuario equals u.IdUsuario
                                    join s in _context.StatusEvento on e.IdStatusEvento equals s.IdStatusEvento
                                    select new
                                    {
                                        e.IdEvento,
                                        u.IdUsuario,
                                        u.Nome,
                                        u.Foto,
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
                                    result.IdEvento,
                                    result.IdUsuario,
                                    result.Nome,
                                    result.Foto,
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
            catch (Exception ex)
            {

                throw new Exception("Ocorreu um erro ao listar os eventos com valor total do usuario", ex); ;
            }
        }

    }
}
