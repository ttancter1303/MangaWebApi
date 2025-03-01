using System;
using System.Collections.Generic;
using MangaWeb.Domain.Enums;
using MangaWeb.Domain.Models.Mangas;

namespace MangaWeb.Domain.Models.Authors
{
    public class AuthorDetailViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
        public List<MangaViewModel> Mangas { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public EntityStatus Status { get; set; }
    }
}