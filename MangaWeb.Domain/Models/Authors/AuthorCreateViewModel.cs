using System.ComponentModel.DataAnnotations;

namespace MangaWeb.Domain.Models.Authors
{
    public class AuthorCreateViewModel
    {
        [Required]
        [StringLength(1000)]
        public string Name { get; set; }

        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}