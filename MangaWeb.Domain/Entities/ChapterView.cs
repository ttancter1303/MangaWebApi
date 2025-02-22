using MangaWeb.Domain.Abstractions;
using MangaWeb.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaWeb.Domain.Entities
{
    [Table("ChapterViews")]
    public class ChapterView : DomainEntity<Guid>, IAuditTable
    {
        public Guid ChapterId { get; set; }

        [ForeignKey(nameof(ChapterId))]
        public Chapter Chapter { get; set; }

        public DateTime ViewDate { get; set; } // Ngày truy cập

        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public EntityStatus Status { get; set; }
    }
}