using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MangaWeb.Domain.Models.Chapters
{
    public class UpdateChapterRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề chapter không được để trống")]
        [StringLength(1000)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Số thứ tự chapter không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Số thứ tự chapter phải lớn hơn 0")]
        public int ChapterNumber { get; set; }

        // Danh sách hình ảnh mới cần thêm vào
        public List<IFormFile> NewImages { get; set; }

        // Danh sách đường dẫn hình ảnh cần xóa
        public List<string> RemoveImagePaths { get; set; }

        // Danh sách vị trí cần thay đổi thứ tự (key: vị trí cũ, value: vị trí mới)
        public Dictionary<int, int> ReorderImages { get; set; }
    }
}