using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiweb.churras.show.Domains
{
    [Table(nameof(Evento))]
    public class Evento
    {
        [Key]
        public Guid IdEvento { get; set; } = Guid.NewGuid();

        [Column(TypeName = "DATETIME")]
        [Required(ErrorMessage = "Data e hora do evento obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? DataHoraEvento { get; set; }

        [Column(TypeName = "INT")]
        [Required(ErrorMessage = "Quantidade de pessoas no evento obrigatório")]
        public int? QuantidadePessoasEvento { get; set; }

        [Column(TypeName = "INT")]
        [Required(ErrorMessage = "Duracao do evento obrigatória")]
        public int? DuracaoEvento { get; set; }

        [Column(TypeName = "BIT")]
        public bool? Descartaveis { get; set; }

        [Column(TypeName = "BIT")]
        public bool? Acompanhamentos { get; set; }

        [Column(TypeName = "INT")]
        public int? Garconete { get; set; }

        [Column(TypeName = "BIT")]
        public bool? Confirmado { get; set; }

        [Column(TypeName = "DATETIME")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? DataDeCriacao { get; set; }

        [Column(TypeName = "DECIMAL(18,2)")] // 18 dígitos de precisão, 2 casas decimais
        public decimal? ValorTotal { get; set; }

        //REFERENCIA COM PACOTE (FK)

        public Guid IdPacotes { get; set; }

        [ForeignKey("IdPacotes")]
        public Pacotes? Pacotes { get; set; }

        //REFERENCIA COM ENDERECO (FK)

        public Guid IdEndereco { get; set; }

        [ForeignKey("IdEndereco")]
        public Endereco? Endereco { get; set; }

        //REFERENCIA COM USUARIO (FK)

        public Guid IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

        //REFERENCIA COM STATUS ORCAMENTO (FK)

        public Guid IdStatusEvento { get; set; }

        [ForeignKey("IdStatusEvento")]
        public StatusEvento? StatusEvento { get; set; }
    }
}
