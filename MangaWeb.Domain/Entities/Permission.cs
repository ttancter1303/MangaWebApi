using MangaWeb.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaWeb.Domain.Entities
{
    [Table("Permissions")]
    public class Permission : DomainEntity<Guid>
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Index { get; set; }
        public string ParentCode { get; set; } = string.Empty;

    }
}