using System.ComponentModel.DataAnnotations;

namespace apiweb.churras.show.ViewModels
{
    public class CriarEventoViewModel
    {
        [Required(ErrorMessage = "Data e hora do evento obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? DataHoraEvento { get; set; }

        [Required(ErrorMessage = "Quantidade de pessoas no evento obrigatório")]
        public int? QuantidadePessoasEvento { get; set; }

        [Required(ErrorMessage = "Duração do evento obrigatória")]
        public int? DuracaoEvento { get; set; }
        public bool? Descartaveis { get; set; }
        public bool? Acompanhamentos { get; set; }
        public int? Garconete { get; set; }
        public bool? Confirmado { get; set; }
        public Guid IdPacotes { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdStatusEvento { get; set; }

        [Required(ErrorMessage = "Logradouro é obrigatório")]
        public string? Logradouro { get; set; }
        
        [Required(ErrorMessage = "Cidade é obrigatória")]
        public string? Cidade { get; set; }
        
        [Required(ErrorMessage = "UF é obrigatória")]
        public string? UF { get; set; }
        
        [Required(ErrorMessage = "CEP é obrigatório")]
        public int? Cep { get; set; }

        [Required(ErrorMessage = "Número é obrigatório")]
        public int? Numero { get; set; }

        public string? Bairro { get; set; }
        public string? Complemento { get; set; }
        
    }
}
