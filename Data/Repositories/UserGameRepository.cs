using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IstimAPI.Data.IRepositories;
using IstimAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IstimAPI.Data.Repositories
{
    public class UserGameRepository : IUserGameRepository
    {
        private readonly DataContext _context;

        public UserGameRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<UserGame>> GetAllUserGamesByUserAsync(string userId)
        {
            return await _context.UserGames
                .AsNoTracking()
                .Include(x => x.Game)
                .Include(x => x.Game.Category)
                .Include(x => x.Game.AgeRange)
                .OrderByDescending(x => x.Id)
                .Where(x => x.ApplicationUserId == userId)
                .ToListAsync();
        }

        public async Task<UserGame> GetUserGameByIdAsync(int userGameId)
        {
            return await _context.UserGames
                .AsNoTracking()
                .Include(x => x.Game)
                .FirstOrDefaultAsync(x => x.Id == userGameId);
        }

        public async Task CreateUserGameAsync(UserGame userGame)
        {
            _context.UserGames.Add(userGame);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserGameAsync(UserGame userGame)
        {
            _context.Entry(userGame).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserGameAsync(int userGameId)
        {
            var userGame = await _context.UserGames
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == userGameId);

            _context.UserGames.Remove(userGame);
            await _context.SaveChangesAsync();
        }

        public bool UserGameByIdExists(int userGameId)
        {
            return _context.UserGames
                .Any(x => x.Id == userGameId);
        }

        public bool UserGameExists(string userId, int gameId)
        {
            return _context.UserGames
                .Any(x => x.ApplicationUserId == userId && x.GameId == gameId);
        }
    }
}