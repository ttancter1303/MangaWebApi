using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Utility;
using System;
using System.Collections.Generic;

namespace MangaWeb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MangasController : ControllerBase
    {
        [Authorize(Policy = CommonConstants.Permissions.VIEW_MANGA_PERMISSION)]
        [HttpGet]
        public IActionResult GetMangas()
        {
            // Logic để lấy danh sách truyện tranh
            var mangas = new List<Manga>
            {
                new Manga { Id = Guid.NewGuid(), Title = "Manga 1", Author = "Author 1" },
                new Manga { Id = Guid.NewGuid(), Title = "Manga 2", Author = "Author 2" }
            };
            return Ok(mangas);
        }

        [Authorize(Policy = CommonConstants.Permissions.ADD_MANGA_PERMISSION)]
        [HttpPost]
        public IActionResult AddManga([FromBody] Manga manga)
        {
            // Logic để thêm truyện tranh
            return Ok(manga);
        }

        [Authorize(Policy = CommonConstants.Permissions.UPDATE_MANGA_PERMISSION)]
        [HttpPut("{id}")]
        public IActionResult UpdateManga(Guid id, [FromBody] Manga manga)
        {
            // Logic để cập nhật truyện tranh
            return Ok(manga);
        }

        [Authorize(Policy = CommonConstants.Permissions.DELETE_MANGA_PERMISSION)]
        [HttpDelete("{id}")]
        public IActionResult DeleteManga(Guid id)
        {
            // Logic để xóa truyện tranh
            return NoContent();
        }
    }
}