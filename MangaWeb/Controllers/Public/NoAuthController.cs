using MangaWeb.Api.Controllers.Base;
using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Models.Commons;
using MangaWeb.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace MangaWeb.Api.Controllers.Public
{

    public class NoAuthController : NoAuthorizeController
    {
        private readonly IUserService _userService;
        public NoAuthController(IUserService userService)
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
        [Route("register-customer")]
        public async Task<ResponseResult> RegisterUser([FromBody] RegisterUserViewModel model)
        {
            var result = await _userService.RegisterUser(model);
            return result;
        }
    }
}
