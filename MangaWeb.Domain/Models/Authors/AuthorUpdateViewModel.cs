using System;
using System.ComponentModel.DataAnnotations;
using MangaWeb.Domain.Enums;

namespace MangaWeb.Domain.Models.Authors
{
    public class AuthorUpdateViewModel
    {

        public Guid Id { get; set; }

        [StringLength(1000)]
        public string Name { get; set; }

        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
        public EntityStatus Status { get; set; }
    }
}