using System.ComponentModel.DataAnnotations.Schema;
using MangaWeb.Domain.Abstractions;

namespace MangaWeb.Domain.Entities
{
    [Table("RolesPermissions")]
    public class RolePermission : DomainEntity<Guid>
	{
        public Guid RoleId { get; set; }
        public string PermissionCode { get; set; } = string.Empty;
    }
}
