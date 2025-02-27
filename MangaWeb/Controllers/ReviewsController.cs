using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Utility;
using System;
using System.Collections.Generic;

namespace MangaWeb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        [Authorize(Policy = CommonConstants.Permissions.VIEW_REVIEW_PERMISSION)]
        [HttpGet("{mangaId}")]
        public IActionResult GetReviews(Guid mangaId)
        {
            // Logic để lấy danh sách đánh giá theo mangaId
            var reviews = new List<ReviewManga>
            {
                new ReviewManga { Id = Guid.NewGuid(), Content = "Great manga!", MangaId = mangaId },
                new ReviewManga { Id = Guid.NewGuid(), Content = "Awesome story!", MangaId = mangaId }
            };
            return Ok(reviews);
        }

        [Authorize(Policy = CommonConstants.Permissions.ADD_REVIEW_PERMISSION)]
        [HttpPost]
        public IActionResult AddReview([FromBody] ReviewManga review)
        {
            // Logic để thêm đánh giá
            return Ok(review);
        }

        [Authorize(Policy = CommonConstants.Permissions.UPDATE_REVIEW_PERMISSION)]
        [HttpPut("{id}")]
        public IActionResult UpdateReview(Guid id, [FromBody] ReviewManga review)
        {
            // Logic để cập nhật đánh giá
            return Ok(review);
        }

        [Authorize(Policy = CommonConstants.Permissions.DELETE_REVIEW_PERMISSION)]
        [HttpDelete("{id}")]
        public IActionResult DeleteReview(Guid id)
        {
            // Logic để xóa đánh giá
            return NoContent();
        }
    }
}