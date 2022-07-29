using System.ComponentModel.DataAnnotations;

namespace IstimAPI.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(2, ErrorMessage = "Informe no mínimo 2 caracteres")]
        [MaxLength(255, ErrorMessage = "Informe no máximo 255 caracteres")]
        public string Title { get; set; }
    }
}