using MangaWeb.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace MangaWeb.Domain.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public EntityStatus Status { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    }
}