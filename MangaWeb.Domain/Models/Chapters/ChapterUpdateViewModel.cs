using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MangaWeb.Domain.Enums;

namespace MangaWeb.Domain.Models.Chapters
{
    public class ChapterUpdateViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Title { get; set; }

        [Required]
        public int ChapterNumber { get; set; }

        [Required]
        public Guid MangaId { get; set; }

        public List<string> ChapterImages { get; set; }
        public EntityStatus Status { get; set; }
    }
}