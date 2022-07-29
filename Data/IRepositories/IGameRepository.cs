using System.Threading.Tasks;
using IstimAPI.Models;
using IstimAPI.Shared;

namespace IstimAPI.Data.IRepositories
{
    public interface IGameRepository
    {
        Task<PagedListCustom<Game>> GetAllGamesAsync(string globalFilter, int pageNumber = 1, int pageSize = 10);
        Task<Game> GetGameByIdAsync(int gameId);
        Task CreateGameAsync(Game game);
        Task UpdateGameAsync(Game game);
        Task DeleteGameAsync(int gameId);
        bool GameExists(int gameId);
    }
}