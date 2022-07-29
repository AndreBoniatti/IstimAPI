using System.Collections.Generic;
using System.Threading.Tasks;
using IstimAPI.Models;

namespace IstimAPI.Data.IRepositories
{
    public interface IUserGameRepository
    {
        Task<List<UserGame>> GetAllUserGamesByUserAsync(string userId);
        Task<UserGame> GetUserGameByIdAsync(int userGameId);
        Task CreateUserGameAsync(UserGame userGame);
        Task UpdateUserGameAsync(UserGame userGame);
        Task DeleteUserGameAsync(int userGameId);
        bool UserGameByIdExists(int userGameId);
        bool UserGameExists(string userId, int gameId);
    }
}