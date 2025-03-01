using System;
using System.ComponentModel.DataAnnotations;

namespace MangaWeb.Domain.Models.Reviews
{
    public class ReviewMangaCreateViewModel
    {
        [Required]
        [StringLength(1000)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public Guid MangaId { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}