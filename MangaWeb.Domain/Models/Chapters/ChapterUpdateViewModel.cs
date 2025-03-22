using MangaWeb.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MangaWeb.Domain.Models.Chapters
{
    public class ChapterUpdateViewModel
    {

        public Guid Id { get; set; }


        [MaxLength(1000)]
        public string Title { get; set; }


        public int ChapterNumber { get; set; }


        public Guid MangaId { get; set; }

        public List<string> ImagePaths { get; set; } = new(); // Danh sách đường dẫn ảnh


        public EntityStatus Status { get; set; }
    }
}
