using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MangaWeb.Domain.Models.Images
{
    public class UploadImageViewModel
    {
        [Required]
        public List<IFormFile> Images { get; set; }
    }
}
