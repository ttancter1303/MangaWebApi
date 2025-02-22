using MangaWeb.Domain.Abstractions;
using MangaWeb.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaWeb.Domain.Entities
{

    [Table("GeneralImages")]
    public class GeneralImage : DomainEntity<Guid>, IAuditTable
    {
        [Column(TypeName = "nvarchar(1000)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string Url { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public EntityStatus Status { get; set; }
    }
}
