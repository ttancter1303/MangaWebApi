using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MangaWeb.Domain.Models.Chapters
{
    public class ChapterCreateViewModel
    {
        [Required]
        [StringLength(1000)]
        public string Title { get; set; }

        [Required]
        public int ChapterNumber { get; set; }

        [Required]
        public Guid MangaId { get; set; }

        public List<string> ChapterImages { get; set; } // URLs or paths to images
    }
}