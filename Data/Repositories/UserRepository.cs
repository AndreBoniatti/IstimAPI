using System.Linq;
using System.Threading.Tasks;
using IstimAPI.Data.IRepositories;
using IstimAPI.Models;
using IstimAPI.Models.Dto.User;
using Microsoft.EntityFrameworkCore;

namespace IstimAPI.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetApplicationUserByIdAsync(string userId)
        {
            return await _context.ApplicationUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<UserInfoDto> GetUserByIdAsync(string userId)
        {
            return await _context.ApplicationUsers
                .AsNoTracking()
                .Where(x => x.Id == userId)
                .Select(x => new UserInfoDto
                {
                    User = x.UserName,
                    Email = x.Email,
                    Phone = x.PhoneNumber,
                    BirthDate = x.BirthDate
                })
                .FirstOrDefaultAsync();
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public bool UserExists(string userId)
        {
            return _context.ApplicationUsers.Any(x => x.Id == userId);
        }
    }
}