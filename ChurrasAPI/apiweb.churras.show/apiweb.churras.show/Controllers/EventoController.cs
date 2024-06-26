﻿using apiweb.churras.show.Domains;
using apiweb.churras.show.Interfaces;
using apiweb.churras.show.Repositories;
using apiweb.churras.show.Utils.Mail;
using apiweb.churras.show.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace apiweb.churras.show.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        private readonly IEventoRepository _eventoRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmailSendingService _emailSendingService;

        public EventoController(IEventoService eventService, IEventoRepository eventRepository, IEnderecoRepository enderecoRepository, IUnitOfWork unitOfWork, EmailSendingService emailSendingService)
        {
            _eventoService = eventService;
            _eventoRepository = eventRepository;
            _enderecoRepository = enderecoRepository;
            _unitOfWork = unitOfWork;
            _emailSendingService = emailSendingService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastarEvento(CriarEventoViewModel eventoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var endereco = new Endereco
            {
                Logradouro = eventoViewModel.Logradouro,
                Cidade = eventoViewModel.Cidade,
                Numero = eventoViewModel.Numero,
            };

            _enderecoRepository.Cadastrar(endereco);
            await _unitOfWork.CommitAsync();

            var evento = new Evento
            {
                DataHoraEvento = eventoViewModel.DataHoraEvento,
                QuantidadePessoasEvento = eventoViewModel.QuantidadePessoasEvento,
                DuracaoEvento = eventoViewModel.DuracaoEvento,
                Descartaveis = eventoViewModel.Descartaveis,
                Acompanhamentos = eventoViewModel.Acompanhamentos,
                Confirmado = eventoViewModel.Confirmado,
                Garconete = eventoViewModel.Garconete,
                IdPacotes = eventoViewModel.IdPacotes,
                IdEndereco = endereco.IdEndereco, 
                IdUsuario = eventoViewModel.IdUsuario,
                IdStatusEvento = eventoViewModel.IdStatusEvento,
                DataDeCriacao = DateTime.Now
            };

            // Calcular o valor total do evento
            evento.ValorTotal = await _eventoService.CalcularValorTotal(evento);

            // Salvar o evento no banco de dados
            _eventoRepository.Cadastrar(evento);

            await _unitOfWork.CommitAsync();

            var claims = HttpContext.User.Claims;
            
            // Obter o email do usuário logado do token JWT
            var emailClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (emailClaim != null)
            {
                string emailUsuario = emailClaim.Value;
                // Enviar email com os detalhes do evento
                await _emailSendingService.SendEventDetailsEmail(emailUsuario, evento);
            }

            return Ok(evento);
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_eventoRepository.ListarTodos());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("ListarEventosValorTotal")]
        public IActionResult ListarEventos()
        {
            try
            {
                Guid id = Guid.Parse(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                var eventos = _eventoService.ListarEventosValorTotal(id);

                if (eventos == null || !eventos.Any())
                {
                    return NotFound();
                }

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("BuscarPorData")]
        public IActionResult ListarPorData([FromQuery] string data)
        {
            try
            {
                if (DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    var events = _eventoService.BuscarPorData(parsedDate);
                    return Ok(events);
                }
                return BadRequest("Data inválida.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("BuscarPorStatus")]
        public async Task<IActionResult> ListarPorStatus(string status)
        {
            try
            {
                var eventos = await _eventoService.BuscarPorStatusAsync(status);
                if (eventos == null || !eventos.Any())
                {
                    return NotFound("Nenhum evento encontrado com o status fornecido.");
                }

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar eventos: {ex.Message}");
            }
        }

        [HttpPut("AtualizarStatus")]
        public IActionResult AtualizarStatusEvento(Guid id, [FromBody] AtualizarStatusEventoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _eventoRepository.AtualizarStatus(id, model);
                    return StatusCode(200);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
