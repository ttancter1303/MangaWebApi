using System;
using MangaWeb.Domain.Enums;

namespace MangaWeb.Domain.Models.Authors
{
    public class AuthorViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? ProfileImageUrl { get; set; }
        public EntityStatus Status { get; set; }
    }
}