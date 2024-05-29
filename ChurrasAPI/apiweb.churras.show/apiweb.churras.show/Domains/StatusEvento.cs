using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiweb.churras.show.Domains
{
    [Table(nameof(StatusEvento))]
    public class StatusEvento
    {
        [Key]
        public Guid IdStatusEvento { get; set; } = Guid.NewGuid();

        [Column(TypeName = "VARCHAR(100)")]
        public string? Status { get; set; }
    }
}
