using System;
using MangaWeb.Domain.Enums;

namespace MangaWeb.Domain.Models.Reviews
{
    public class ReviewMangaDetailViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public Guid MangaId { get; set; }
        public string MangaTitle { get; set; } // Title of the related Manga
        public Guid UserId { get; set; }
        public string UserName { get; set; } // Name of the user who reviewed
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public EntityStatus Status { get; set; }
    }
}