using System.Collections.Generic;
using System.Threading.Tasks;
using IstimAPI.Data.IRepositories;
using IstimAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace IstimAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserGameController : ControllerBase
    {
        private readonly IUserGameRepository _userGameRepository;
        private readonly IGameRepository _gameRepository;

        public UserGameController(
            IUserGameRepository userGameRepository,
            IGameRepository gameRepository
        )
        {
            _userGameRepository = userGameRepository;
            _gameRepository = gameRepository;
        }

        [HttpGet("UserId/{id}")]
        public async Task<ActionResult<List<UserGame>>> GetAllUserGamesByUser(string id)
        {
            var userGames = await _userGameRepository
                .GetAllUserGamesByUserAsync(id);

            return Ok(userGames);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserGame>> GetUserGameById(int id)
        {
            if (!UserGameByIdExists(id))
                return NotFound(new { Message = "Este recurso não existe" });

            var userGame = await _userGameRepository.GetUserGameByIdAsync(id);

            return Ok(userGame);
        }

        [HttpPost]
        public async Task<IActionResult> PostUserGame([FromBody] List<UserGame> userGames)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            foreach (var userGame in userGames)
            {
                if (UserGameExists(userGame.ApplicationUserId, userGame.GameId))
                {
                    var game = await _gameRepository.GetGameByIdAsync(userGame.GameId);
                    return BadRequest(new { Message = $"O jogo {game.Title} já foi adquirido" });
                }
            }

            try
            {
                foreach (var userGame in userGames)
                {
                    await _userGameRepository.CreateUserGameAsync(userGame);
                }

                return Created("", "");
            }
            catch
            {
                return BadRequest(new { Message = "Ocorreu algum erro, tente novamente" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutUserGame(int id, [FromBody] UserGame userGame)
        {
            if (!UserGameByIdExists(id))
                return NotFound(new { Message = "Este recurso não existe" });

            if (id != userGame.Id)
                return NotFound(new { Message = "Recurso não encontrado" });

            try
            {
                await _userGameRepository.UpdateUserGameAsync(userGame);
                return NoContent();
            }
            catch
            {
                return BadRequest(new { Message = "Não foi possível atualizar este recurso" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUserGame(int id)
        {
            if (!UserGameByIdExists(id))
                return NotFound(new { Message = "Este recurso não existe" });

            try
            {
                await _userGameRepository.DeleteUserGameAsync(id);
                return NoContent();
            }
            catch
            {
                return BadRequest(new { Message = "Não foi possível remover este recurso" });
            }
        }

        private bool UserGameByIdExists(int userGameId)
        {
            return _userGameRepository.UserGameByIdExists(userGameId);
        }

        private bool UserGameExists(string userId, int gameId)
        {
            return _userGameRepository.UserGameExists(userId, gameId);
        }
    }
}