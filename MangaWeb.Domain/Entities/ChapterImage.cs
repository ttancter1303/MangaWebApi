using MangaWeb.Domain.Abstractions;
using MangaWeb.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaWeb.Domain.Entities
{
    [Table("ChapterImages")]
    public class ChapterImage : DomainEntity<Guid>, IAuditTable
    {
        public int PageNumber { get; set; } // Số thứ tự trang (ví dụ: trang 1, trang 2, ...)

        [Column(TypeName = "nvarchar(1000)")]
        public string ImageUrl { get; set; } // Đường dẫn đến ảnh

        public Guid ChapterId { get; set; }

        [ForeignKey(nameof(ChapterId))]
        public Chapter Chapter { get; set; }

        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public EntityStatus Status { get; set; }
    }
}