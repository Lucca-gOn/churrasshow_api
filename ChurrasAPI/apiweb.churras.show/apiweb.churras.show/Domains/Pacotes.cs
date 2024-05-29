using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiweb.churras.show.Domains
{
    [Table(nameof(Pacotes))]
    public class Pacotes
    {
        [Key]
        public Guid IdPacotes { get; set; } = Guid.NewGuid();

        [Column(TypeName = "VARCHAR(100)")]
        public string? NomePacote { get; set; }

        [Column(TypeName = "TEXT")]
        public string? DescricaoPacote { get; set; }

        [Column(TypeName = "DECIMAL(18,2)")] // 18 dígitos de precisão, 2 casas decimais
        public decimal? ValorPorPessoa { get; set; }
    }
}
