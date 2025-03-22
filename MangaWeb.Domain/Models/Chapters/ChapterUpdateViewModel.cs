using MangaWeb.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MangaWeb.Domain.Models.Chapters
{
    public class ChapterUpdateViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Title { get; set; }

        [Required]
        public int ChapterNumber { get; set; }

        [Required]
        public Guid MangaId { get; set; }

        public List<string> ImagePaths { get; set; } = new(); // Danh sách đường dẫn ảnh


        public EntityStatus Status { get; set; }
    }
}
