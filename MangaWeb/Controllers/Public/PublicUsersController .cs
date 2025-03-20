using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MangaWeb.Api.Controllers.Public
{
    [ApiController]
    [Route("api/public/users")]
    public class PublicUsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public PublicUsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserViewModel model)
        {
            var result = await _userService.RegisterUser(model);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var result = await _userService.Login(model);
            return Ok(result);
        }
    }
}
