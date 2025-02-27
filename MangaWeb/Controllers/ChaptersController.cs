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
    public class ChaptersController : ControllerBase
    {
        [Authorize(Policy = CommonConstants.Permissions.VIEW_CHAPTER_PERMISSION)]
        [HttpGet("{mangaId}")]
        public IActionResult GetChapters(Guid mangaId)
        {
            // Logic để lấy danh sách chương truyện theo mangaId
            var chapters = new List<Chapter>
            {
                new Chapter { Id = Guid.NewGuid(), Title = "Chapter 1", MangaId = mangaId },
                new Chapter { Id = Guid.NewGuid(), Title = "Chapter 2", MangaId = mangaId }
            };
            return Ok(chapters);
        }

        [Authorize(Policy = CommonConstants.Permissions.ADD_CHAPTER_PERMISSION)]
        [HttpPost]
        public IActionResult AddChapter([FromBody] Chapter chapter)
        {
            // Logic để thêm chương truyện
            return Ok(chapter);
        }

        [Authorize(Policy = CommonConstants.Permissions.UPDATE_CHAPTER_PERMISSION)]
        [HttpPut("{id}")]
        public IActionResult UpdateChapter(Guid id, [FromBody] Chapter chapter)
        {
            // Logic để cập nhật chương truyện
            return Ok(chapter);
        }

        [Authorize(Policy = CommonConstants.Permissions.DELETE_CHAPTER_PERMISSION)]
        [HttpDelete("{id}")]
        public IActionResult DeleteChapter(Guid id)
        {
            // Logic để xóa chương truyện
            return NoContent();
        }
    }
}