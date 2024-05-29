using apiweb.churras.show.Context;
using apiweb.churras.show.Domains;
using apiweb.churras.show.Dto;
using apiweb.churras.show.Interfaces;

namespace apiweb.churras.show.Service
{

    public class UsuarioService : IUsuarioService
    {
        private readonly ChurrasShowContext _context;

        public UsuarioService(ChurrasShowContext context)
        {
            _context = context;
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
