using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaWeb.Domain.Models.Chapters
{
    public class ChapterCreateViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int ChapterNumber { get; set; }

        [Required]
        public Guid MangaId { get; set; }

        [Required]
        public List<string> ImageUrls { get; set; } = new(); 

    }

}
