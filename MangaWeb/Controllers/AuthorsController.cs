﻿using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Application.Services;
using MangaWeb.Domain.Models.Authors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MangaWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
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

        [HttpGet("search")]
        public async Task<IActionResult> SearchAuthors([FromQuery] AuthorSearchViewModel searchModel)
        {
            var authors = await _authorService.SearchAuthorsAsync(searchModel);
            return Ok(authors);
        }
    }
}