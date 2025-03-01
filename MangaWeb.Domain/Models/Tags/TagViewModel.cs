using System;
using MangaWeb.Domain.Enums;

namespace MangaWeb.Domain.Models.Tags
{
    public class TagViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public EntityStatus Status { get; set; }
    }
}