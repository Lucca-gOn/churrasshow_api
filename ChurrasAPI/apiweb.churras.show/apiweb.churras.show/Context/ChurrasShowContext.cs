using apiweb.churras.show.Domains;
using Microsoft.EntityFrameworkCore;

namespace apiweb.churras.show.Context
{
    public class ChurrasShowContext : DbContext
    {
        public ChurrasShowContext(DbContextOptions options) : base(options)
        {
        }

        //DEFINIR AS ENTIDADES NO BANCO
        public DbSet<Comentarios> Comentario { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Evento> Evento { get; set; }
        public DbSet<Pacotes> Pacote { get; set; }
        public DbSet<StatusEvento> StatusEvento { get; set; }
        public DbSet<TiposUsuario> TiposUsuario { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
