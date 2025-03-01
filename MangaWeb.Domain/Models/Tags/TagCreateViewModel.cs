using System.ComponentModel.DataAnnotations;

namespace MangaWeb.Domain.Models.Tags
{
    public class TagCreateViewModel
    {
        [Required]
        [StringLength(1000)]
        public string Name { get; set; }
    }
}