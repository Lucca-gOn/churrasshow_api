using apiweb.churras.show.Domains;
using apiweb.churras.show.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using System.Text;

namespace apiweb.churras.show.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioRepository _comentarioRepository;
        private readonly ContentModeratorClient _contentModeratorClient;

        public ComentarioController(IComentarioRepository comentarioRepository, ContentModeratorClient contentModeratorClient)
        {
            _comentarioRepository = comentarioRepository;
            _contentModeratorClient = contentModeratorClient;
        }

        [HttpPost("ComentarioIA")]
        public async Task<IActionResult> PostIA(Comentarios novoComentario)
        {
            try
            {
                if (string.IsNullOrEmpty(novoComentario.DescricaoComentario))
                {
                    return BadRequest("A descrição do comentário está nula ou vazia.");
                }

                using var stream = new MemoryStream(Encoding.UTF8.GetBytes(novoComentario.DescricaoComentario));

                var moderationResult = await _contentModeratorClient.TextModeration
                    .ScreenTextAsync("text/plain", stream, "por", false, false, null, true);

                if (moderationResult.Terms != null && moderationResult.Terms.Any())
                {
                    novoComentario.Exibe = false;
                }
                else
                {
                    novoComentario.Exibe = true;
                }

                // Supondo que o método Cadastrar() seja assíncrono
                _comentarioRepository.Cadastrar(novoComentario);

                return StatusCode(201, novoComentario);
            }
            catch (Exception erro)
            {
                return BadRequest($"Ocorreu um erro ao processar a solicitação: {erro.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post(Comentarios novoComentario)
        {
            try
            {
                _comentarioRepository.Cadastrar(novoComentario);
                return StatusCode(201);
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_comentarioRepository.Listar());
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        [HttpGet("ListarComentariosValidos")]
        public IActionResult GetIA()
        {
            try
            {
                return Ok(_comentarioRepository.ListarSomenteExibe());
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                return Ok(_comentarioRepository.BuscarPorId(id));
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _comentarioRepository.Deletar(id);
                return Ok();
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid id, Comentarios comentario)
        {
            try
            {
                // Presume-se que a descrição do comentário pode ser nula ou vazia, então verificamos se ela não é nula ou vazia antes de moderar.
                if (!string.IsNullOrEmpty(comentario.DescricaoComentario))
                {
                    using var stream = new MemoryStream(Encoding.UTF8.GetBytes(comentario.DescricaoComentario));
                    var moderationResult = await _contentModeratorClient.TextModeration
                        .ScreenTextAsync("text/plain", stream, "por", false, false, null, true);

                    // Se o resultado da moderação contiver termos ofensivos, 'Exibe' é definido como false.
                    comentario.Exibe = moderationResult.Terms == null || !moderationResult.Terms.Any();
                }
                else
                {
                    // Se o comentário for nulo ou vazio, considere não ofensivo e defina 'Exibe' como true.
                    comentario.Exibe = true;
                }

                // Atualiza o comentário no banco de dados com o novo valor de 'Exibe'.
                _comentarioRepository.Atualizar(id, comentario);
                return StatusCode(200, comentario);
            }
            catch (Exception erro)
            {
                // Aqui você poderia registrar o erro antes de devolver a resposta.
                return BadRequest(erro.Message);
            }
        }
    }
}