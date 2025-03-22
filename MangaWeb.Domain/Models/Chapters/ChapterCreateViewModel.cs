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

        public string Title { get; set; }


        public int ChapterNumber { get; set; }


        public Guid MangaId { get; set; }


        public List<string> ImageUrls { get; set; } = new(); 

    }

}
