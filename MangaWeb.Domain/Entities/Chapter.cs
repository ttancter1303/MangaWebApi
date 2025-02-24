using MangaWeb.Domain.Abstractions;
using MangaWeb.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaWeb.Domain.Entities
{
    [Table("Chapters")]
    public class Chapter : DomainEntity<Guid>, IAuditTable
    {
        [Column(TypeName = "nvarchar(1000)")]
        public string Title { get; set; }

        public int ChapterNumber { get; set; } // Số thứ tự chapter (ví dụ: Chapter 1, Chapter 2, ...)

        public Guid MangaId { get; set; }

        [ForeignKey(nameof(MangaId))]
        public Manga Manga { get; set; }

        public ICollection<ChapterImage> ChapterImages { get; set; } = new List<ChapterImage>(); // Danh sách ảnh trong chapter

        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public EntityStatus Status { get; set; }
    }
}