using MangaWeb.Domain.Enums;
using System;
using System.Collections.Generic;

namespace MangaWeb.Domain.Models.Mangas
{
    public class MangaViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public string AuthorName { get; set; }
        public List<string> Tags { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public EntityStatus Status { get; set; }
    }
}