using MangaWeb.Domain.Exceptions;
using MangaWeb.Domain.Models.Users;
using MangaWeb.Domain.Utility;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MangaWeb.Api.Filters
{
    public class PermissionAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _permissions;
        /// <param name="permissions"></param>
        public PermissionAttribute(
            params string[] permissions)
        {
            _permissions = permissions;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var currentUser = HelperUtility.GetValueHeader<UserProfileModel>(context.HttpContext.Request,CommonConstants.Header.CurrentUser);
            var userPermissions = currentUser?.Permissions;

            if (userPermissions == null)
            {
                throw new UserException.ForbiddenException();
            }
            else
            {
                foreach (var permission in _permissions)
                {
                    if (!userPermissions.Any(s => s.Equals(permission, StringComparison.OrdinalIgnoreCase)))
                    {
                        throw new UserException.ForbiddenException();
                    }
                }
            }
        }
       
    }

}