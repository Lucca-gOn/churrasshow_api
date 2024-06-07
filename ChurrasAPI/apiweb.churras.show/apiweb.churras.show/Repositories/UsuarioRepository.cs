using apiweb.churras.show.Context;
using apiweb.churras.show.Domains;
using apiweb.churras.show.Dto;
using apiweb.churras.show.Interfaces;
using apiweb.churras.show.Utils;
using apiweb.churras.show.ViewModels;

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

        public void Atualizar(Guid id, AtualizarUsuarioViewModel dadosAtualizados)
        {
            try
            {
                Usuario usuarioBuscado = _context.Usuario.FirstOrDefault(x => x.IdUsuario == id)!;

                if (usuarioBuscado != null)
                {
                    // Atualiza as informações
                    usuarioBuscado.Nome = dadosAtualizados.Nome;
                    usuarioBuscado.Email = dadosAtualizados.Email;
                    usuarioBuscado.Endereco.Logradouro = dadosAtualizados.Logradouro;
                    usuarioBuscado.Endereco.Cidade = dadosAtualizados.Cidade;
                    usuarioBuscado.Endereco.UF = dadosAtualizados.Uf;
                    usuarioBuscado.Endereco.CEP = dadosAtualizados.Cep;
                    usuarioBuscado.Endereco.Numero = dadosAtualizados.Numero;
                    usuarioBuscado.Endereco.Bairro = dadosAtualizados.Bairro;
                    usuarioBuscado.Endereco.Complemento = dadosAtualizados.Complemento;
                    usuarioBuscado.Foto = dadosAtualizados.Foto;

                    _context.Update(usuarioBuscado); // Adiciona o Update para garantir que as mudanças sejam rastreadas.
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException?.ToString() ?? e.Message);
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
            return _context.Usuario.FirstOrDefault(x => x.IdUsuario == id)!;
        }

        public void Cadastrar(Usuario novoUsuario)
        {
            try
            {
                novoUsuario.Senha = Criptografia.GerarHash(novoUsuario.Senha!);
                _context.Add(novoUsuario);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
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
