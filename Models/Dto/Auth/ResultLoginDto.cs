using IstimAPI.Models.Dto.User;

namespace IstimAPI.Models.Dto.Auth
{
    public class ResultLoginDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}