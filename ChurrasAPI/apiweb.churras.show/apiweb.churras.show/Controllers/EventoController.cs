using apiweb.churras.show.Domains;
using apiweb.churras.show.Interfaces;
using apiweb.churras.show.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

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

        public EventoController(IEventoService eventService, IEventoRepository eventRepository, IEnderecoRepository enderecoRepository, IUnitOfWork unitOfWork)
        {
            _eventoService = eventService;
            _eventoRepository = eventRepository;
            _enderecoRepository = enderecoRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CadastarEvento([FromForm] CriarEventoViewModel eventoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var endereco = new Endereco
            {
                Logradouro = eventoViewModel.Logradouro,
                Cidade = eventoViewModel.Cidade,
                UF = eventoViewModel.UF,
                CEP = eventoViewModel.Cep,
                Numero = eventoViewModel.Numero,
                Bairro = eventoViewModel.Bairro,
                Complemento = eventoViewModel.Complemento
            };

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

            // Salvar o endereço no banco de dados
            _enderecoRepository.Cadastrar(endereco);

            // Salvar o evento no banco de dados
            _eventoRepository.Cadastrar(evento);
            await _unitOfWork.CommitAsync();

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
                // Obtém o ID do usuário logado dos claims do JWT
                Guid id = Guid.Parse(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                // Chama o serviço para listar eventos com base no ID do usuário logado
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
    }
}
