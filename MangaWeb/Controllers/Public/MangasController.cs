using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Application.Services;
using MangaWeb.Domain.Models.Mangas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MangaWeb.Api.Controllers.Public
{
    [ApiController]
    [Route("api/[controller]")]
    public class MangasController : ControllerBase
    {
        private readonly IMangaService _mangaService;
        private readonly IMangaViewService _mangaViewService;

        public MangasController(IMangaService mangaService, IMangaViewService mangaViewService)
        {
            _mangaService = mangaService;
            _mangaViewService = mangaViewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMangas()
        {
            var mangas = await _mangaService.GetAllMangasAsync();
            return Ok(mangas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMangaById(Guid id)
        {
            // Record the view
            await _mangaViewService.RecordMangaViewAsync(id);

            var manga = await _mangaService.GetMangaByIdAsync(id);
            if (manga == null)
            {
                return NotFound();
            }
            return Ok(manga);
        }

        [HttpPost]
        public async Task<IActionResult> CreateManga([FromBody] MangaCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mangaId = await _mangaService.CreateMangaAsync(model);
            return CreatedAtAction(nameof(GetMangaById), new { id = mangaId }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManga(Guid id, [FromBody] MangaUpdateViewModel model)
        {
            if (!ModelState.IsValid || id != model.Id)
            {
                return BadRequest(ModelState);
            }

            await _mangaService.UpdateMangaAsync(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManga(Guid id)
        {
            await _mangaService.DeleteMangaAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMangas([FromQuery] MangaSearchViewModel searchModel)
        {
            var mangas = await _mangaService.SearchMangasAsync(searchModel);
            return Ok(mangas);
        }
    }
}