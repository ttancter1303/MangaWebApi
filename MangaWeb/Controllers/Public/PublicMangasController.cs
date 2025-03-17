using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Application.Services;
using MangaWeb.Domain.Models.Mangas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MangaWeb.Api.Controllers.Base;

namespace MangaWeb.Api.Controllers.Public
{
    [ApiController]
    [Route("api/public/mangas")]
    public class PublicMangasController : NoAuthorizeController
    {
        private readonly IMangaService _mangaService;
        private readonly IMangaViewService _mangaViewService;

        public PublicMangasController(IMangaService mangaService, IMangaViewService mangaViewService)
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
            await _mangaViewService.RecordMangaViewAsync(id);
            var manga = await _mangaService.GetMangaByIdAsync(id);
            if (manga == null)
            {
                return NotFound();
            }
            return Ok(manga);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMangas([FromQuery] MangaSearchViewModel searchModel)
        {
            var mangas = await _mangaService.SearchMangasAsync(searchModel);
            return Ok(mangas);
        }
    }
}
