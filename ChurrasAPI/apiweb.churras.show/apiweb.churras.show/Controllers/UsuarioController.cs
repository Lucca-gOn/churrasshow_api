using apiweb.churras.show.Domains;
using apiweb.churras.show.Interfaces;
using apiweb.churras.show.Repositories;
using apiweb.churras.show.Utils.BlobStorage;
using apiweb.churras.show.Utils.Mail;
using apiweb.churras.show.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiweb.churras.show.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly EmailSendingService _emailSendingService;


        public UsuarioController(IUsuarioRepository usuarioRepository, IUsuarioService usuarioService, EmailSendingService emailSendingService)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
            _emailSendingService = emailSendingService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CriarUsuarioClienteViewModel clienteModel)
        {
            try
            {
                Usuario novoUsuario = new Usuario();

                novoUsuario.Nome = clienteModel.Nome;
                novoUsuario.Email = clienteModel.Email;
                novoUsuario.RG = clienteModel.Rg;
                novoUsuario.CPF = clienteModel.Cpf;
                novoUsuario.IdTipoUsuario = clienteModel.IdTipoUsuario;

                

                novoUsuario.Foto = await AzureBlobStorageHelper.UploadImageBlobAsync(clienteModel.Arquivo!, connectionString, containerName);

                novoUsuario.Senha = clienteModel.Senha;

                novoUsuario.Endereco = new Endereco();

                novoUsuario.Endereco.Logradouro = clienteModel.Logradouro;
                novoUsuario.Endereco.Cidade = clienteModel.Cidade;
                novoUsuario.Endereco.UF = clienteModel.Uf;
                novoUsuario.Endereco.CEP = clienteModel.Cep;
                novoUsuario.Endereco.Numero = clienteModel.Numero;
                novoUsuario.Endereco.Bairro = clienteModel.Bairro;
                novoUsuario.Endereco.Complemento = clienteModel.Complemento;

                _usuarioRepository.Cadastrar(novoUsuario);

                await _emailSendingService.SendWelcomeEmail(novoUsuario.Email!, novoUsuario.Nome!);

                return Ok();
            }
            catch (Exception ex)

            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_usuarioService.ListarUsuarios().Itens);
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        [HttpPut("AlterarSenha")]
        public IActionResult UpdatePassword(string email, AlterarSenhaViewModel senha)
        {
            try
            {
                _usuarioRepository.AlterarSenha(email, senha.SenhaNova!);

                return Ok("Senha alterada com sucesso !");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AtualizarUsuario")]
        public IActionResult AtualizarUsuario(Guid id, [FromBody] AtualizarUsuarioViewModel model)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    _usuarioRepository.AtualizarUsuario(id, model); 
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

        [HttpPut("AlterarFotoPerfil")]
        public async Task<IActionResult> UpdateProfileImage(Guid id, [FromForm] UsuarioViewModel form)
        {
            try
            {

                Usuario usuarioBuscado = _usuarioRepository.BuscarPorId(id);


                if (usuarioBuscado == null)
                {
                    return NotFound();
                }


                

                string fotoUrl = await AzureBlobStorageHelper.UploadImageBlobAsync(form.Arquivo!, connectionString!, containerName!);


                usuarioBuscado.Foto = fotoUrl;

                _usuarioRepository.AtualizarFoto(id, fotoUrl);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult ListarUsuarioPorId(Guid id)
        {
            try
            {
                var usuario = _usuarioService.ListarUsuarioId(id);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
