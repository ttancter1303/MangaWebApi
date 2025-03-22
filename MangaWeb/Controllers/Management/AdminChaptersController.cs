using MangaWeb.Api.Controllers.Base;
using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Models.Chapters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MangaWeb.Api.Controllers.Management
{
    [ApiController]
    [Route("api/admin/chapters")]
    public class AdminChaptersController : AuthorizeController
    {
        private readonly IChapterService _chapterService;

        public AdminChaptersController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChapter([FromBody] ChapterCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdChapter = await _chapterService.CreateChapterAsync(model);
            return CreatedAtAction(nameof(GetChapterById), new { id = createdChapter.Id }, createdChapter);
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

        [HttpGet("manga/{mangaId}")]
        public async Task<IActionResult> GetChaptersByMangaId(Guid mangaId)
        {
            var chapters = await _chapterService.GetChaptersByMangaIdAsync(mangaId);
            return Ok(chapters);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChapter(Guid id, [FromBody] ChapterUpdateViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest("Chapter ID mismatch");
            }

            if (!ModelState.IsValid)
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
