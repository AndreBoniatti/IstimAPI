using System.Linq;
using System.Threading.Tasks;
using IstimAPI.Data.IRepositories;
using IstimAPI.Data.Queries;
using IstimAPI.Models;
using IstimAPI.Shared;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace IstimAPI.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly DataContext _context;

        public GameRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedListCustom<Game>> GetAllGamesAsync(
            string globalFilter, int pageNumber = 1, int pageSize = 10
        )
        {
            int totalCount = await _context.Games
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .CountAsync();

            return new PagedListCustom<Game>()
            {
                TotalCount = totalCount,
                Data = await _context.Games
                    .AsNoTracking()
                    .Include(x => x.Category)
                    .Include(x => x.AgeRange)
                    .Where(GameQueries.GetGames(globalFilter))
                    .OrderByDescending(x => x.Id)
                    .ToPagedListAsync(pageNumber, pageSize)
            };
        }

        public async Task<Game> GetGameByIdAsync(int gameId)
        {
            return await _context.Games
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.AgeRange)
                .FirstOrDefaultAsync(x => x.Id == gameId);
        }

        public async Task CreateGameAsync(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGameAsync(Game game)
        {
            _context.Entry(game).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGameAsync(int gameId)
        {
            var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == gameId);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }

        public bool GameExists(int gameId)
        {
            return _context.Games.Any(x => x.Id == gameId);
        }
    }
}