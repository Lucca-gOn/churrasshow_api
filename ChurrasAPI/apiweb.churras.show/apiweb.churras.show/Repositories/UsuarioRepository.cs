using apiweb.churras.show.Context;
using apiweb.churras.show.Domains;
using apiweb.churras.show.Dto;
using apiweb.churras.show.Interfaces;
using apiweb.churras.show.Utils;
using apiweb.churras.show.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace apiweb.churras.show.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ChurrasShowContext _context;

        public UsuarioRepository(ChurrasShowContext context)
        {
            _context = context;
        }

        public bool AlterarSenha(string email, string senhaNova)
        {
            try
            {
                var user = _context.Usuario.FirstOrDefault(x => x.Email == email);

                if (user == null) return false;

                user.Senha = Criptografia.GerarHash(senhaNova);

                _context.Update(user);

                _context.SaveChanges();

                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AtualizarUsuario(Guid id, AtualizarUsuarioViewModel model)
        {
            try
            {
                Usuario usuarioExistente = _context.Usuario.Include(u => u.Endereco).FirstOrDefault(u => u.IdUsuario == id)!;

                if (usuarioExistente != null)
                {
                    usuarioExistente.Nome = model.name;
                    usuarioExistente.RG = model.rg;
                    usuarioExistente.CPF = model.cpf;

                    if (model.logradouro != null && usuarioExistente.Endereco != null)
                    {
                        usuarioExistente.Endereco.Logradouro = model.logradouro;
                        usuarioExistente.Endereco.Cidade = model.cidade;
                        usuarioExistente.Endereco.UF = model.uf;
                        usuarioExistente.Endereco.CEP = model.cep ?? usuarioExistente.Endereco.CEP; 
                        usuarioExistente.Endereco.Numero = model.number ?? usuarioExistente.Endereco.Numero;
                        usuarioExistente.Endereco.Bairro = model.bairro;
                        usuarioExistente.Endereco.Complemento = model.complemento;
                    }

                    _context.Update(usuarioExistente);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar usuario", ex);
            }
        }



        public void AtualizarFoto(Guid id, string novaUrlFoto)
        {
            try
            {
                Usuario usuarioBuscado = _context.Usuario.FirstOrDefault(x => x.IdUsuario == id)!;

                if (usuarioBuscado != null)
                {
                    usuarioBuscado.Foto = novaUrlFoto;
                }

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.ToString());
            }
        }

        public Usuario BuscarPorEmailESenha(string email, string senha)
        {
            try
            {
                var user = _context.Usuario.Select(u => new Usuario
                {
                    IdUsuario = u.IdUsuario,
                    Email = u.Email,
                    Senha = u.Senha,
                    Nome = u.Nome,
                    Foto = u.Foto,
                    TiposUsuario = new TiposUsuario
                    {
                        IdTipoUsuario = u.TiposUsuario!.IdTipoUsuario,
                        TituloTipoUsuario = u.TiposUsuario.TituloTipoUsuario
                    }
                }).FirstOrDefault
                (x => x.Email == email);

                if (user == null) return null!;

                if (!Criptografia.CompararHash(senha, user.Senha!)) return null!;

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Usuario BuscarPorId(Guid id)
        {
            return _context.Usuario
                       .Include(u => u.Endereco)
                       .Include(u => u.TiposUsuario)
                       .FirstOrDefault(u => u.IdUsuario == id)!;
        }

        public void Cadastrar(Usuario novoUsuario)
        {
            try
            {
                novoUsuario.Senha = Criptografia.GerarHash(novoUsuario.Senha!);
                _context.Add(novoUsuario);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("IX_Usuario_Email") ?? false)
                {
                    throw new InvalidOperationException("O e-mail fornecido já está em uso.");
                }
                else if (ex.InnerException?.Message.Contains("IX_Usuario_CPF") ?? false)
                {
                    throw new InvalidOperationException("O CPF fornecido já está em uso.");
                }
                else if (ex.InnerException?.Message.Contains("IX_Usuario_RG") ?? false)
                {
                    throw new InvalidOperationException("O RG fornecido já está em uso.");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao processar cadastro do usuário.", ex);
            }
        }

        public void Deletar(Guid idUsuario)
        {
            throw new NotImplementedException();
        }

        public ListarUsuariosResponse ListarUsuarios()
        {
            throw new NotImplementedException();
        }
    }
}
