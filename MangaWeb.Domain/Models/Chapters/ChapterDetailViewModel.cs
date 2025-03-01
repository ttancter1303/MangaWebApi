using System;
using System.Collections.Generic;
using MangaWeb.Domain.Enums;

namespace MangaWeb.Domain.Models.Chapters
{
    public class ChapterDetailViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int ChapterNumber { get; set; }
        public Guid MangaId { get; set; }
        public string MangaTitle { get; set; } // Title of the related Manga
        public List<string> ChapterImages { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public EntityStatus Status { get; set; }
    }
}