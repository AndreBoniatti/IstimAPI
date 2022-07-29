using System.Threading.Tasks;
using IstimAPI.Data.IRepositories;
using IstimAPI.Models;
using IstimAPI.Shared;
using Microsoft.AspNetCore.Mvc;

namespace IstimAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;

        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PagedListCustom<Game>>> GetAllGames(
            [FromQuery] string globalFilter, int pageNumber = 1, int pageSize = 10
        )
        {
            var games = await _gameRepository.GetAllGamesAsync(globalFilter, pageNumber, pageSize);

            return Ok(games);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Game>> GetGameById(int id)
        {
            if (!GameExists(id))
                return NotFound(new { Message = "Este jogo não existe" });

            var game = await _gameRepository.GetGameByIdAsync(id);

            return Ok(game);
        }

        [HttpPost]
        public async Task<IActionResult> PostGame([FromBody] Game game)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                game.IsActive = true;

                await _gameRepository.CreateGameAsync(game);
                return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game);
            }
            catch
            {
                return BadRequest(new { Message = "Não foi possível criar este jogo" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutGame(int id, [FromBody] Game game)
        {
            if (!GameExists(id))
                return NotFound(new { Message = "Este jogo não existe" });

            if (id != game.Id)
                return NotFound(new { Message = "Jogo não encontrado" });

            try
            {
                game.IsActive = true;

                await _gameRepository.UpdateGameAsync(game);
                return NoContent();
            }
            catch
            {
                return BadRequest(new { Message = "Não foi possível atualizar este jogo" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            if (!GameExists(id))
                return NotFound(new { Message = "Este jogo não existe" });

            try
            {
                await _gameRepository.DeleteGameAsync(id);
                return NoContent();
            }
            catch
            {
                return BadRequest(new { Message = "Não foi possível remover este jogo" });
            }
        }

        private bool GameExists(int gameId)
        {
            return _gameRepository.GameExists(gameId);
        }
    }
}