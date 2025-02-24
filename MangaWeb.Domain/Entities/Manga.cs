using MangaWeb.Domain.Abstractions;
using MangaWeb.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaWeb.Domain.Entities
{
    [Table("Mangas")]
    public class Manga : DomainEntity<Guid>, IAuditTable
    {
        [Column(TypeName = "nvarchar(1000)")]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string? Description { get; set; }

        [Column(TypeName = "ntext")]
        public string? CoverImageUrl { get; set; }

        public Guid AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }

        public ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public ICollection<ReviewManga> ReviewMangas { get; set; } = new List<ReviewManga>();

        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public EntityStatus Status { get; set; }
    }
}