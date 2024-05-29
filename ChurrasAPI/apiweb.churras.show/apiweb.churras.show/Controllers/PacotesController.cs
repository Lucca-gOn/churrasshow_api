using apiweb.churras.show.Domains;
using apiweb.churras.show.Interfaces;
using apiweb.churras.show.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiweb.churras.show.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PacotesController : ControllerBase
    {
        private readonly IPacoteRepository _pacoteRepository;
        public PacotesController(IPacoteRepository pacoteRepository)
        {
            _pacoteRepository = pacoteRepository;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_pacoteRepository.ListarTodos());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(Pacotes novoPacote)
        {
            try
            {
                _pacoteRepository.Cadastrar(novoPacote);
                return StatusCode(201);
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPacoteById(Guid id)
        {
            try
            {
                var pacote = await _pacoteRepository.BuscarPorId(id);
                if (pacote == null)
                {
                    return NotFound();
                }
                return Ok(pacote);
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }
    }
}
