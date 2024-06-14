using apiweb.churras.show.Context;
using apiweb.churras.show.Domains;
using apiweb.churras.show.Dto;
using apiweb.churras.show.Interfaces;
using apiweb.churras.show.Repositories;

namespace apiweb.churras.show.Service
{

    public class UsuarioService : IUsuarioService
    {
        private readonly ChurrasShowContext _context;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(ChurrasShowContext context, IUsuarioRepository usuarioRepository)
        {
            _context = context;
            _usuarioRepository = usuarioRepository;
        }

        public ListarUsuariosResponseItem ListarUsuarioId(Guid id)
        {
            var usuario = _usuarioRepository.BuscarPorId(id);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            return new ListarUsuariosResponseItem(
                usuario.IdUsuario,
                usuario.Endereco.IdEndereco,
                usuario.Nome!,
                usuario.Email!,
                usuario.RG!,
                usuario.CPF!,
                usuario.Foto!,
                usuario.TiposUsuario!.TituloTipoUsuario!,
                usuario.Endereco.Logradouro!,
                usuario.Endereco.Cidade!,
                usuario.Endereco.UF!,
                usuario.Endereco.CEP.ToString()!,
                usuario.Endereco.Numero.ToString()!,
                usuario.Endereco.Bairro!,
                usuario.Endereco.Complemento!
            );
        }

        public ListarUsuariosResponse ListarUsuarios()
        {
            var listaUsuarios = (from u in _context.Usuario
                                 join e in _context.Endereco on u.IdEndereco equals e.IdEndereco
                                 select new ListarUsuariosResponseItem(
                                     u.IdUsuario,
                                     u.IdEndereco,
                                     u.Nome!,
                                     u.Email!,
                                     u.RG!,
                                     u.CPF!,
                                     u.Foto!,
                                     u.TiposUsuario!.TituloTipoUsuario!,
                                     e.Logradouro!,
                                     e.Cidade!,
                                     e.UF!,
                                     e.CEP.ToString()!,
                                     e.Numero.ToString()!,
                                     e.Bairro!,
                                     e.Complemento!
                                 )).ToList().AsReadOnly();

            return new ListarUsuariosResponse(listaUsuarios);
        }

    }
}
