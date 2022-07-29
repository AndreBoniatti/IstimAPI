using System.Threading.Tasks;
using IstimAPI.Data.IRepositories;
using IstimAPI.Models;
using IstimAPI.Models.Dto.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IstimAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;

        public UserController(
            UserManager<ApplicationUser> userManager,
            IUserRepository userRepository
        )
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfoDto>> GetUserById(string id)
        {
            if (!UserExists(id))
                return NotFound(new { Message = "Este usuário não existe" });

            var user = await _userRepository.GetUserByIdAsync(id);

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] CreateUserDto applicationUser)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(applicationUser.Email);

                if (user == null)
                {
                    var newUser = new ApplicationUser
                    {
                        UserName = applicationUser.User,
                        Email = applicationUser.Email,
                        BirthDate = applicationUser.BirthDate,
                        PhoneNumber = applicationUser.Phone,
                        EmailConfirmed = true,
                        Role = "USER"
                    };

                    await _userManager.CreateAsync(newUser, applicationUser.Password);
                    return Created("", "");
                }
                else
                {
                    return BadRequest(new { Message = "Este usuário já existe" });
                }
            }
            catch
            {
                return BadRequest(new { Message = "Não foi possível criar este usuário" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, [FromBody] UserInfoDto userInfo)
        {
            if (!UserExists(id))
                return NotFound(new { Message = "Este usuário não existe" });

            var user = await _userRepository.GetApplicationUserByIdAsync(id);
            user.UpdateUser(userInfo);

            try
            {
                await _userRepository.UpdateUserAsync(user);
                return NoContent();
            }
            catch
            {
                return BadRequest(new { Message = "Não foi possível atualizar este usuário" });
            }
        }

        private bool UserExists(string userId)
        {
            return _userRepository.UserExists(userId);
        }
    }
}