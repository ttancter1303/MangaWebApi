using MangaWeb.Domain.Abstractions;
using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaWeb.Domain.Entities
{
    [Table("Authors")]
    public class Author : DomainEntity<Guid>, IAuditTable
    {
        [Column(TypeName = "nvarchar(1000)")]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string? Bio { get; set; }

        [Column(TypeName = "ntext")]
        public string? ProfileImageUrl { get; set; }

        public ICollection<Manga> Mangas { get; set; } = new List<Manga>();

        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public EntityStatus Status { get; set; }
    }
}