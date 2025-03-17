using MangaWeb.Api.Controllers.Base;
using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Models.Authors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MangaWeb.Api.Controllers.Public
{
    public class PublicAuthorsController : NoAuthorizeController
    {
        private readonly IAuthorService _authorService;

        public PublicAuthorsController(IAuthorService authorService)
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

        [HttpGet("search")]
        public async Task<IActionResult> SearchAuthors([FromQuery] AuthorSearchViewModel searchModel)
        {
            var authors = await _authorService.SearchAuthorsAsync(searchModel);
            return Ok(authors);
        }
    }
}
