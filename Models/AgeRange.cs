using System.ComponentModel.DataAnnotations;

namespace IstimAPI.Models
{
    public class AgeRange
    {
        public AgeRange() { }

        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(1, ErrorMessage = "Informe no mínimo 1 caractere")]
        [MaxLength(255, ErrorMessage = "Informe no máximo 255 caracteres")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int Range { get; set; }
    }
}