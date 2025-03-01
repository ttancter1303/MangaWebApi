using System;
using System.Collections.Generic;
using MangaWeb.Domain.Enums;

namespace MangaWeb.Domain.Models.Chapters
{
    public class ChapterViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int ChapterNumber { get; set; }
        public Guid MangaId { get; set; }
        public List<string> ChapterImages { get; set; } // URLs or paths to images
        public EntityStatus Status { get; set; }
    }
}