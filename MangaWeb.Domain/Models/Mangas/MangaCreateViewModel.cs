using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MangaWeb.Domain.Models.Mangas
{
    public class MangaCreateViewModel
    {
        [Required]
        [StringLength(1000)]
        public string Title { get; set; }

        public string? Description { get; set; }

        public IFormFile? CoverImage { get; set; } // Thêm ảnh upload

        public string? CoverImageUrl { get; set; } // Lưu đường dẫn ảnh

        public Guid AuthorId { get; set; }
        public List<Guid> TagIds { get; set; }
    }
}
