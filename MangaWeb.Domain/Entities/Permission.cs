using MangaWeb.Domain.Abstractions;

namespace MangaWeb.Domain.Entities
{
    public class Permission : DomainEntity<Guid>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}