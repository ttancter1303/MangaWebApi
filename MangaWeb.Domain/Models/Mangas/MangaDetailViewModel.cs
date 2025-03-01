using MangaWeb.Domain.Enums;
using MangaWeb.Domain.Models.Chapters;
using MangaWeb.Domain.Models.Reviews;
using System;
using System.Collections.Generic;

namespace MangaWeb.Domain.Models.Mangas
{
    public class MangaDetailViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public string AuthorName { get; set; }
        public List<string> Tags { get; set; }
        public List<ChapterViewModel> Chapters { get; set; }
        public List<ReviewMangaViewModel> Reviews { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public EntityStatus Status { get; set; }
    }
}