using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Application.Services;
using MangaWeb.Domain.Models.Mangas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MangaWeb.Api.Controllers.Base;
using MangaWeb.Api.Controllers.Public;

namespace MangaWeb.Api.Controllers.Management
{
    [ApiController]
    [Route("api/admin/mangas")]
    public class AdminMangasController : AuthorizeController
    {
        private readonly IMangaService _mangaService;

        public AdminMangasController(IMangaService mangaService)
        {
            _mangaService = mangaService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateManga([FromBody] MangaCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mangaId = await _mangaService.CreateMangaAsync(model);
            return CreatedAtAction(nameof(PublicMangasController.GetMangaById), "PublicMangas", new { id = mangaId }, model);
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
    }
}
