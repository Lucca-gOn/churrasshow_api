using apiweb.churras.show.Interfaces;
using apiweb.churras.show.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiweb.churras.show.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class StatusEventoController : ControllerBase
    {
        private readonly IStatusEventoRepository _statusEventoRepository;
        public StatusEventoController(IStatusEventoRepository statusEventoRepository)
        {
            _statusEventoRepository = statusEventoRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_statusEventoRepository.ListarTodos());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
