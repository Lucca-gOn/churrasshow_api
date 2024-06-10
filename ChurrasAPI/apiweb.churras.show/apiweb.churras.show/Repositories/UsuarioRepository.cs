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

        public void Atualizar(Guid id, Usuario usuario)
        {
            try
            {
                Usuario buscarClinica = _context.Usuario.Find(id);

                if (buscarClinica != null)
                {
                    buscarClinica.RG = usuario.RG;
                    buscarClinica.CPF = usuario.CPF;

                    if (usuario.Endereco != null && buscarClinica.Endereco != null)
                    {
                        buscarClinica.Endereco.Logradouro = usuario.Endereco.Logradouro;
                        buscarClinica.Endereco.Cidade = usuario.Endereco.Cidade;
                        buscarClinica.Endereco.UF = usuario.Endereco.UF;
                        buscarClinica.Endereco.CEP = usuario.Endereco.CEP;
                        buscarClinica.Endereco.Numero = usuario.Endereco.Numero;
                        buscarClinica.Endereco.Bairro = usuario.Endereco.Bairro;
                        buscarClinica.Endereco.Complemento = usuario.Endereco.Complemento;
                    }

                    _context.Update(buscarClinica);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
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
