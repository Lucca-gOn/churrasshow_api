using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiweb.churras.show.Domains
{
    [Table(nameof(Usuario))]
    [Index(nameof(RG), IsUnique = true)]
    [Index(nameof(CPF), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class Usuario
    {
        [Key]
        public Guid IdUsuario { get; set; } = Guid.NewGuid();
        
        [Column(TypeName = "VARCHAR(100)")]
        [Required(ErrorMessage ="Nome de usuario obrigatório!")]
        public string? Nome { get; set; }

        [Column(TypeName = "VARCHAR(9)")]
        [Required(ErrorMessage ="RG do usuario obrigatório")]
        public string? RG { get; set; }

        [Column(TypeName = "VARCHAR(11)")]
        [Required(ErrorMessage = "CPF do usuario obrigatório")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter apenas números.")]
        public string? CPF { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        [Required(ErrorMessage = "Email do usuario obrigatório")]
        public string? Email { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        [Required(ErrorMessage = "Senha do usuario obrigatório")]
        public string? Senha { get; set; }

        [Column(TypeName = "TEXT")]
        public string? Foto { get; set; }

        [Column(TypeName = "INT")]
        public int? CodRecupSenha { get; set; }

        //REFERENCIA COM TIPO USUARIO (FK)

        [Required(ErrorMessage = "TipoUsuário obrigatório")]
        public Guid IdTipoUsuario { get; set; }

        [ForeignKey("IdTipoUsuario")]
        public TiposUsuario? TiposUsuario { get; set; }

        //REFERENCIA COM ENDERECO (FK)

        public Guid IdEndereco { get; set; }

        [ForeignKey("IdEndereco")]
        public Endereco? Endereco { get; set; }
    }
}
