using System;
using System.Collections.Generic;
using MangaWeb.Domain.Enums;
using MangaWeb.Domain.Models.Mangas;

namespace MangaWeb.Domain.Models.Tags
{
    public class TagDetailViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<MangaViewModel> Mangas { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public EntityStatus Status { get; set; }
    }
}