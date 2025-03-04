using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MangaWeb.Domain.Models.Chapters
{
    public class CreateChapterRequest
    {
        [Required(ErrorMessage = "Tiêu đề chapter không được để trống")]
        [StringLength(1000)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Số thứ tự chapter không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Số thứ tự chapter phải lớn hơn 0")]
        public int ChapterNumber { get; set; }

        [Required(ErrorMessage = "MangaId không được để trống")]
        public Guid MangaId { get; set; }

        [Required(ErrorMessage = "Cần ít nhất một hình ảnh cho chapter")]
        public List<IFormFile> Images { get; set; }
    }
}