using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Application.Services;
using MangaWeb.Domain.Models.Tags;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MangaWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
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

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] TagCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tagId = await _tagService.CreateTagAsync(model);
            return CreatedAtAction(nameof(GetTagById), new { id = tagId }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(Guid id, [FromBody] TagUpdateViewModel model)
        {
            if (!ModelState.IsValid || id != model.Id)
            {
                return BadRequest(ModelState);
            }

            await _tagService.UpdateTagAsync(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            await _tagService.DeleteTagAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchTags([FromQuery] TagSearchViewModel searchModel)
        {
            var tags = await _tagService.SearchTagsAsync(searchModel);
            return Ok(tags);
        }
    }
}