using System;
using System.ComponentModel.DataAnnotations;
using MangaWeb.Domain.Enums;

namespace MangaWeb.Domain.Models.Tags
{
    public class TagUpdateViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Name { get; set; }

        public EntityStatus Status { get; set; }
    }
}