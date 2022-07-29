using System.ComponentModel.DataAnnotations;

namespace IstimAPI.Models
{
    public class UserGame
    {
        public int Id { get; set; }

        public decimal? Rating { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}