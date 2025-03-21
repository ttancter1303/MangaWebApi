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
            var currentUser = HelperUtility.GetValueHeader<UserProfileModel>(
                context.HttpContext.Request, CommonConstants.Header.CurrentUser
            );

            if (currentUser == null)
            {
                Console.WriteLine("Không tìm thấy thông tin người dùng trong request header.");
                throw new UserException.ForbiddenException();
            }

            Console.WriteLine($"User ID: {currentUser.UserId}");
            Console.WriteLine($"Permissions: {string.Join(", ", currentUser.Permissions ?? new List<string>())}");

            var userPermissions = currentUser.Permissions;

            if (userPermissions == null || !userPermissions.Any())
            {
                Console.WriteLine("User không có quyền nào.");
                throw new UserException.ForbiddenException();
            }

            foreach (var permission in _permissions)
            {
                if (!userPermissions.Any(s => s.Equals(permission, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine($"User thiếu quyền: {permission}");
                    throw new UserException.ForbiddenException();
                }
            }
        }


    }

}