using Microsoft.AspNetCore.Mvc;
using MangaWeb.Domain.Models.Reviews;
using MangaWeb.Domain.Exceptions;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MangaWeb.Application.Services;
using MangaWeb.Domain.Abstractions.ApplicationServices;


namespace MangaWeb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewMangaController : ControllerBase
    {
        private readonly IReviewMangaService _reviewService;

        public ReviewMangaController(IReviewMangaService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewMangaViewModel>>> GetAllReviews()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewMangaViewModel>> GetReviewById(Guid id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
                return NotFound();
                
            return Ok(review);
        }

        [HttpGet("manga/{mangaId}")]
        public async Task<ActionResult<IEnumerable<ReviewMangaViewModel>>> GetReviewsByMangaId(Guid mangaId)
        {
            var reviews = await _reviewService.GetReviewsByMangaIdAsync(mangaId);
            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult<ReviewMangaViewModel>> CreateReview(ReviewMangaViewModel reviewModel)
        {
            try
            {
                var createdReview = await _reviewService.CreateReviewAsync(reviewModel);
                return CreatedAtAction(nameof(GetReviewById), new { id = createdReview.Id }, createdReview);
            }
            catch (System.IO.InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(Guid id, ReviewMangaViewModel reviewModel)
        {
            if (id != reviewModel.Id)
                return BadRequest("ID không khớp");

            try
            {
                await _reviewService.UpdateReviewAsync(reviewModel);
                return NoContent();
            }
            catch (System.IO.InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            try
            {
                await _reviewService.DeleteReviewAsync(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}