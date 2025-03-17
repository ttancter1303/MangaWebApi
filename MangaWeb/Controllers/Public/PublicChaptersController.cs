using MangaWeb.Api.Controllers.Base;
using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Models.Chapters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MangaWeb.Api.Controllers.Public
{
    public class PublicChaptersController : NoAuthorizeController
    {
        private readonly IChapterService _chapterService;

        public PublicChaptersController(IChapterService chapterService)
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
    }
}
