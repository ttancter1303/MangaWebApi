using System;
using MangaWeb.Domain.Enums;

namespace MangaWeb.Domain.Models.Reviews
{
    public class ReviewMangaViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public Guid MangaId { get; set; }
        public Guid UserId { get; set; }
        public EntityStatus Status { get; set; }
    }
}