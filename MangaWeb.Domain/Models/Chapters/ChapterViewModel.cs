using System;
using MangaWeb.Domain.Enums;

namespace MangaWeb.Domain.Models.Chapters
{
    public class ChapterViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int ChapterNumber { get; set; }
        public Guid MangaId { get; set; }
        public string[] Images { get; set; }
        public int PageCount { get; set; }
        public string StorageLocation { get; set; }
        public long TotalSize { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public EntityStatus Status { get; set; }
    }
}