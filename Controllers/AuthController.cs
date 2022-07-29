using System.Linq;
using System.Threading.Tasks;
using IstimAPI.Models;
using IstimAPI.Models.Dto.Auth;
using IstimAPI.Models.Dto.User;
using IstimAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IstimAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("Authenticate")]
        public async Task<ActionResult<ResultLoginDto>> Login(
            [FromBody] LoginDto login,
            [FromServices] TokenService tokenService
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null) return BadRequest("Email ou senha inváidos");

            var result = await _signInManager
                .PasswordSignInAsync(user, login.Password, false, true);

            if (result.Succeeded)
            {
                var token = tokenService.GenerateToken(user);

                return Ok(new ResultLoginDto
                {
                    User = new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Username = user.UserName
                    },
                    Token = token
                });
            }
            else
                return Unauthorized("Email ou senha inváidos");
        }
    }
}