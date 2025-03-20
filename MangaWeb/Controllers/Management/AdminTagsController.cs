using MangaWeb.Api.Controllers.Base;
using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Models.Tags;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MangaWeb.Api.Controllers.Management
{
    [ApiController]
    [Route("api/admin/tags")]
    public class AdminTagsController : AuthorizeController
    {
        private readonly ITagService _tagService;

        public AdminTagsController(ITagService tagService)
        {
            _tagService = tagService;
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
    }
}
