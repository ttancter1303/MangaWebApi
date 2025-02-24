using MangaWeb.Domain.Entities;

public class RolePermission
{
    public Guid RoleId { get; set; }
    public AppRole Role { get; set; }  // Quan hệ với AppRole

    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; }  // Quan hệ với Permission
}
