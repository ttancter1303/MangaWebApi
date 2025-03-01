using MangaWeb.Application.Services;
using MangaWeb.Domain.Models.Chapters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MangaWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChaptersController : ControllerBase
    {
        private readonly IChapterService _chapterService;

        public ChaptersController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChapters()
        {
            var chapters = await _chapterService.GetAllChaptersAsync();
            return Ok(chapters);
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

        [HttpPost]
        public async Task<IActionResult> CreateChapter([FromBody] ChapterCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chapterId = await _chapterService.CreateChapterAsync(model);
            return CreatedAtAction(nameof(GetChapterById), new { id = chapterId }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChapter(Guid id, [FromBody] ChapterUpdateViewModel model)
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