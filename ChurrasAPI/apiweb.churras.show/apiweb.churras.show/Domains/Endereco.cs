using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiweb.churras.show.Domains
{
    [Table(nameof(Endereco))]
    public class Endereco
    {
        [Key]
        public Guid IdEndereco { get; set; } = Guid.NewGuid();

        [Column(TypeName = "VARCHAR(100)")]
        public string? Logradouro { get; set; }
        
        [Column(TypeName = "VARCHAR(50)")]
        public string? Cidade { get; set; }
        
        [Column(TypeName = "VARCHAR(2)")]
        public string? UF { get; set; }
        
        [Column(TypeName = "INT")]
        public int? CEP { get; set; }

        [Column(TypeName = "INT")]
        public int? Numero { get; set; }
        
        [Column(TypeName = "VARCHAR(100)")]
        public string? Bairro { get; set; }
        
        [Column(TypeName = "VARCHAR(100)")]
        public string? Complemento { get; set; }

    }
}
