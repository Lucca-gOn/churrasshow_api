using apiweb.churras.show.Context;
using apiweb.churras.show.Utils.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiweb.churras.show.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RecuperarSenhaController : ControllerBase
    {
        private readonly ChurrasShowContext _context;
        private readonly EmailSendingService _emailSendingService;
        public RecuperarSenhaController(ChurrasShowContext context, EmailSendingService emailSendingService)
        {
            _context = context;
            _emailSendingService = emailSendingService;
        }

        [HttpPost]
        public async Task<IActionResult> SendRecoveryCodePassword(string email)
        {
            try
            {
                var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == email);
                if (usuario == null)
                {
                    return NotFound("Usuario não encontrado!");
                }

                Random random = new Random();

                int codigoRecuperacao = random.Next(1000, 9999);
                usuario.CodRecupSenha = codigoRecuperacao;

                await _context.SaveChangesAsync();

                await _emailSendingService.SendRecoveryPassword(email, codigoRecuperacao);

                return Ok("Código de confirmação enviado com sucesso!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ValidarCodigoRecuperacaoSenha")]
        public async Task<IActionResult> ValidatePasswordRecoveryCode(string email, int codigo)
        {
            try
            {
                var user = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    return NotFound("Usuário não encontrado!");
                }
                if (user.CodRecupSenha != codigo)
                {
                    return BadRequest("Código de recuperação é inválido!");
                }

                user.CodRecupSenha = null;

                await _context.SaveChangesAsync();

                return Ok("Código de recuperação está correto!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}