
using MangaWeb.Domain.Abstractions;
using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaWeb.Domain.Entities
{
    [Table("Tags")]
    public class Tag : DomainEntity<Guid>, IAuditTable
    {
        [Column(TypeName = "nvarchar(1000)")]
        public string Name { get; set; }

        public ICollection<Manga> Mangas { get; set; } = new List<Manga>();

        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public EntityStatus Status { get; set; }
    }
}