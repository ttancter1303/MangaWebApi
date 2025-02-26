using System.ComponentModel.DataAnnotations;

namespace MangaWeb.Domain.Models.Images
{
    public class UpdateImageViewModel
    {
        [Required]
        public Guid ImageId { get; set; }

        [Required]
        public string ImageName { get; set; }
    }
}
