using MangaWeb.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MangaWeb.Domain.Models.Mangas
{
    public class MangaUpdateViewModel
    {

        public Guid Id { get; set; }


        public string Title { get; set; }

        public string? Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public Guid AuthorId { get; set; }
        public List<Guid> TagIds { get; set; }
        public EntityStatus Status { get; set; }
    }
}