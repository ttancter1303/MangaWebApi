using MangaWeb.Domain.Abstractions;
using MangaWeb.Domain.Enums;
using System;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaWeb.Domain.Entities
{
    [Table("Chapters")]
    public class Chapter : DomainEntity<Guid>, IAuditTable
    {
        [Required]
        [Column(TypeName = "nvarchar(1000)")]
        public string Title { get; set; }

        [Required]
        public int ChapterNumber { get; set; }

        [Required]
        public Guid MangaId { get; set; }

        [ForeignKey(nameof(MangaId))]
        public Manga Manga { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string ImagePaths { get; set; } // Lưu dạng JSON array của các đường dẫn ảnh

        [NotMapped]
        public string[] Images
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ImagePaths))
                    return Array.Empty<string>();

                try
                {
                    return JsonSerializer.Deserialize<string[]>(ImagePaths) ?? Array.Empty<string>();
                }
                catch (JsonException)
                {
                    return Array.Empty<string>(); // Trả về mảng rỗng nếu JSON không hợp lệ
                }
            }
            set
            {
                ImagePaths = value is { Length: > 0 }
                    ? JsonSerializer.Serialize(value)
                    : string.Empty; // Nếu mảng rỗng, lưu giá trị rỗng thay vì chuỗi JSON
            }
        }

        public int PageCount { get; set; } // Số trang của chapter

        [Column(TypeName = "nvarchar(255)")]
        public string StorageLocation { get; set; } // Vị trí lưu trữ (có thể là local, cloud storage, CDN...)

        [Required]
        public long TotalSize { get; set; } // Tổng dung lượng của chapter (bytes)

        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public EntityStatus Status { get; set; }
    }
}