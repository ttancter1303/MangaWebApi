using System;
using MangaWeb.Domain.Enums;

namespace MangaWeb.Domain.Models.Chapters
{
    public class ChapterDetailViewModel : ChapterViewModel
    {
        public string MangaTitle { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedByUserName { get; set; }
    }
}