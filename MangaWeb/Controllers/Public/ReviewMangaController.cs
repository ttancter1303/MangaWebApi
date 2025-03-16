using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Models.Reviews;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MangaWeb.Api.Controllers.Public
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewMangaController : ControllerBase
    {
        private readonly IReviewMangaService _reviewMangaService;

        public ReviewMangaController(IReviewMangaService reviewMangaService)
        {
            _reviewMangaService = reviewMangaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _reviewMangaService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(Guid id)
        {
            var review = await _reviewMangaService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] ReviewMangaCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdReview = await _reviewMangaService.CreateReviewAsync(model);
            return CreatedAtAction(nameof(GetReviewById), new { id = createdReview.Id }, createdReview);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(Guid id, [FromBody] ReviewMangaUpdateViewModel model)
        {
            if (!ModelState.IsValid || id != model.Id)
            {
                return BadRequest(ModelState);
            }

            await _reviewMangaService.UpdateReviewAsync(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            await _reviewMangaService.DeleteReviewAsync(id);
            return NoContent();
        }
    }
}
