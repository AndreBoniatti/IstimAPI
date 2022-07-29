using System;
using System.ComponentModel.DataAnnotations;
using IstimAPI.Models.Dto.User;
using Microsoft.AspNetCore.Identity;

namespace IstimAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public DateTime BirthDate { get; set; }

        public void UpdateUser(UserInfoDto userInfo)
        {
            PhoneNumber = userInfo.Phone;
        }
    }
}