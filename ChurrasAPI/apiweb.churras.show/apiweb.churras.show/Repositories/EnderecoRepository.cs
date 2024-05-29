using apiweb.churras.show.Context;
using apiweb.churras.show.Domains;
using apiweb.churras.show.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace apiweb.churras.show.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly ChurrasShowContext _context;

        public EnderecoRepository(ChurrasShowContext context)
        {
            _context = context;
        }
        public void Atualizar(Guid Id, Endereco endereco)
        {
            try
            {
                Endereco buscarEndereco = _context.Endereco.Find(Id)!;
                if (buscarEndereco != null)
                {
                    buscarEndereco.Logradouro = endereco.Logradouro;
                    buscarEndereco.Cidade = endereco.Cidade;
                    buscarEndereco.UF = endereco.UF;
                    buscarEndereco.CEP = endereco.CEP;
                    buscarEndereco.Numero = endereco.Numero;
                    buscarEndereco.Bairro = endereco.Bairro;
                    buscarEndereco.Complemento = endereco.Complemento;

                    _context.Update(buscarEndereco);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Cadastrar(Endereco novoEndereco)
        {

            try
            {
                _context.Add(novoEndereco);
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Deletar(Guid id)
        {
            try
            {
                _context.Endereco.Where(e => e.IdEndereco == id).ExecuteDelete();
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Endereco> ListarTodos()
        {
            try
            {
                return _context.Endereco.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
