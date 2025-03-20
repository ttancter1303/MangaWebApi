using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Models.Commons;
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

        [HttpPost]
        [Route("login")]
        public async Task<AuthorizedResponseModel> Login([FromBody] LoginViewModel model)
        {
            var result = await _userService.Login(model);
            return result;
        }

        [HttpPost]
        [Route("register-user")]
        public async Task<ResponseResult> RegisterUser([FromBody] RegisterUserViewModel model)
        {
            var result = await _userService.RegisterUser(model);
            return result;
        }
        [HttpGet("debug-token")]
        public IActionResult DebugToken()
        {
            var token = Request.Headers["Authorization"].ToString();
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();

            return Ok(new { token, claims });
        }

    }
}
