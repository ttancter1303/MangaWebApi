using System;
using System.Collections.Generic;

namespace MangaWeb.Domain.Models.Mangas
{
    public class MangaListViewModel
    {
        public List<MangaViewModel> Mangas { get; set; }
        public int TotalCount { get; set; }
    }
}