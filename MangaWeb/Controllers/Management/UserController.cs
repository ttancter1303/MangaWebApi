using MangaWeb.Api.Controllers.Base;
using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Models.Commons;
using MangaWeb.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MangaWeb.Api.Controllers.Management
{
    public class UserController : AuthorizeController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        [Route("get-profile")]
        public async Task<UserProfileModel> GetUserProfile()
        {
            var result = await _userService.GetUserProfile(CurrentUser.UserName);
            return result;
        }

        [HttpPost]
        [Route("update-profile")]
        public async Task<ResponseResult> UpdateUserInfo([FromBody] UpdateUserInfoViewModel model)
        {
            var result = await _userService.UpdateUserInfo(model, CurrentUser);
            return result;
        }

        [HttpPost]
        [Route("change-password")]
        public async Task<ResponseResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            var result = await _userService.ChangePassword(model, CurrentUser);
            return result;
        }

    }
}
