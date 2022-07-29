using System.Threading.Tasks;
using IstimAPI.Models;
using IstimAPI.Models.Dto.User;

namespace IstimAPI.Data.IRepositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetApplicationUserByIdAsync(string userId);
        Task<UserInfoDto> GetUserByIdAsync(string userId);
        Task UpdateUserAsync(ApplicationUser user);
        bool UserExists(string userId);
    }
}