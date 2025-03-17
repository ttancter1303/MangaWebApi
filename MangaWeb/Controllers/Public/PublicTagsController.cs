using MangaWeb.Api.Controllers.Base;
using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Models.Tags;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MangaWeb.Api.Controllers.Public
{
    public class PublicTagsController : NoAuthorizeController
    {
        private readonly ITagService _tagService;

        public PublicTagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _tagService.GetAllTagsAsync();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagById(Guid id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchTags([FromQuery] TagSearchViewModel searchModel)
        {
            var tags = await _tagService.SearchTagsAsync(searchModel);
            return Ok(tags);
        }
    }
}
