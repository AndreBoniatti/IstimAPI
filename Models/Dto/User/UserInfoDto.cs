using System;

namespace IstimAPI.Models.Dto.User
{
    public class UserInfoDto
    {
        public string User { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
    }
}