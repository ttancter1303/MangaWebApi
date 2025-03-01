using MangaWeb.Domain.Abstractions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaWeb.Domain.Entities
{
    [Table("ChapterImages")]
    public class ChapterImage : DomainEntity<Guid>
    {
        public Guid ChapterId { get; set; }

        [ForeignKey(nameof(ChapterId))]
        public Chapter Chapter { get; set; }

        public string ImageUrl { get; set; } // URL hoặc đường dẫn tới hình ảnh

        public int Order { get; set; } // Thứ tự của hình ảnh trong chapter
    }
}