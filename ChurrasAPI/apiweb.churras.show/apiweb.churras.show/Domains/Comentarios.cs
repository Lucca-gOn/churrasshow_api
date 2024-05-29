using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiweb.churras.show.Domains
{
    [Table(nameof(Comentarios))]
    public class Comentarios
    {
        [Key]
        public Guid IdComentario { get; set; } = Guid.NewGuid();

        [Column(TypeName = "TEXT")]
        public string? DescricaoComentario { get; set; }

        [Column(TypeName = "INT")]
        public int? Pontuacao { get; set; }

        [Column(TypeName = "BIT")]
        [Required]
        public bool Exibe { get; set; }

        //REFERENCIA COM EVENTO (FK)

        public Guid IdEvento { get; set; }

        [ForeignKey("IdEvento")]
        public Evento? Evento { get; set; }

        //REFERENCIA COM USUARIO (FK)

        public Guid IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

    }
}
