using MangaWeb.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace MangaWeb.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string? FullName { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public EntityStatus Status { get; set; }
    }
}