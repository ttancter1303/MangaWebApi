using MangaWeb.Api.Filters;
using MangaWeb.Domain.Models.Users;
using MangaWeb.Domain.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Linq;

namespace MangaWeb.Api.Controllers.Base
{
    [ApiController]
    [ApplicationAuthorize]
    public class AuthorizeController : ControllerBase
    {
        public string UserName => HttpContext.User.Claims
                                    .FirstOrDefault(i => i.Type == "UserName")?.Value ?? string.Empty;

        public string Email => HttpContext.User.Claims
                                  .FirstOrDefault(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value ?? string.Empty;

        public string AccessToken
        {
            get
            {
                var authorization = HttpContext.Request.Headers[HeaderNames.Authorization].ToString();
                return string.IsNullOrEmpty(authorization) ? string.Empty : authorization.Substring(7);
            }
        }

        public UserProfileModel CurrentUser
        {
            get
            {
                var currentUser = HelperUtility.GetValueHeader<UserProfileModel>(HttpContext.Request, CommonConstants.Header.CurrentUser);
                return currentUser ?? new UserProfileModel();
            }
        }
    }
}
