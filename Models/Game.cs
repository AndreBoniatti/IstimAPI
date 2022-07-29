using System;
using System.ComponentModel.DataAnnotations;

namespace IstimAPI.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(2, ErrorMessage = "Informe no mínimo 2 caracteres")]
        [MaxLength(255, ErrorMessage = "Informe no máximo 255 caracteres")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(2, ErrorMessage = "Informe no mínimo 2 caracteres")]
        [MaxLength(500, ErrorMessage = "Informe no máximo 500 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public decimal Value { get; set; }

        public string VideoURL { get; set; }

        public bool IsActive { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int AgeRangeId { get; set; }
        public AgeRange AgeRange { get; set; }
    }
}