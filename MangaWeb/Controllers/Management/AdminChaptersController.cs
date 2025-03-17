using MangaWeb.Api.Controllers.Base;
using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Models.Chapters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MangaWeb.Api.Controllers.Management
{
    public class AdminChaptersController : AuthorizeController
    {
        private readonly IChapterService _chapterService;

        public AdminChaptersController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChapter([FromBody] CreateChapterRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chapterId = await _chapterService.CreateChapterAsync(model);
            return CreatedAtAction(nameof(GetChapterById), new { id = chapterId }, model);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChapterById(Guid id)
        {
            var chapter = await _chapterService.GetChapterByIdAsync(id);
            if (chapter == null)
            {
                return NotFound();
            }
            return Ok(chapter);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChapter(Guid id, [FromBody] UpdateChapterRequest model)
        {
            if (!ModelState.IsValid || id != model.Id)
            {
                return BadRequest(ModelState);
            }

            await _chapterService.UpdateChapterAsync(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChapter(Guid id)
        {
            await _chapterService.DeleteChapterAsync(id);
            return NoContent();
        }
    }
}
