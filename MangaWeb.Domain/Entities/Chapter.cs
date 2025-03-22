using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Chapters")]
public class Chapter
{
    [Required]
    [Column(TypeName = "nvarchar(1000)")]
    public string Title { get; set; }
    [Required]
    public Guid Id { get; set; }
    [Required]
    public int ChapterNumber { get; set; }

    [Required]
    public Guid MangaId { get; set; }

    [ForeignKey(nameof(MangaId))]
    public Manga Manga { get; set; }

    [Column(TypeName = "nvarchar(max)")]  
    public List<string> ImagePaths { get; set; } = new();

    [Column(TypeName = "nvarchar(255)")]

    public DateTime? CreatedDate { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public EntityStatus Status { get; set; }
}
