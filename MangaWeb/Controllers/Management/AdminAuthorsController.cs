using MangaWeb.Api.Controllers.Base;
using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Models.Authors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MangaWeb.Api.Controllers.Management
{
    [ApiController]
    [Route("api/admin/authors")]
    public class AdminAuthorsController : AuthorizeController
    {
        private readonly IAuthorService _authorService;

        public AdminAuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authorId = await _authorService.CreateAuthorAsync(model);
            return CreatedAtAction(nameof(GetAuthorById), new { id = authorId }, model);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(Guid id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] AuthorUpdateViewModel model)
        {
            if (!ModelState.IsValid || id != model.Id)
            {
                return BadRequest(ModelState);
            }

            await _authorService.UpdateAuthorAsync(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            await _authorService.DeleteAuthorAsync(id);
            return NoContent();
        }
    }
}
