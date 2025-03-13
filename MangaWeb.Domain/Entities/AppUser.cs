using MangaWeb.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace MangaWeb.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>
    {

        public bool IsSystemUser { get; set; }
    }
}