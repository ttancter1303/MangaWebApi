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
    public class ImagesController : ControllerBase
    {
        [Authorize(Policy = CommonConstants.Permissions.VIEW_IMAGE_PERMISSION)]
        [HttpGet("{chapterId}")]
        public IActionResult GetImages(Guid chapterId)
        {
            // Logic để lấy danh sách ảnh theo chapterId
            var images = new List<ChapterImage>
            {
                new ChapterImage { Id = Guid.NewGuid(), ImageUrl = "/images/chapter1/page1.jpg", ChapterId = chapterId },
                new ChapterImage { Id = Guid.NewGuid(), ImageUrl = "/images/chapter1/page2.jpg", ChapterId = chapterId }
            };
            return Ok(images);
        }

        [Authorize(Policy = CommonConstants.Permissions.ADD_IMAGE_PERMISSION)]
        [HttpPost]
        public IActionResult AddImage([FromBody] ChapterImage image)
        {
            // Logic để thêm ảnh
            return Ok(image);
        }

        [Authorize(Policy = CommonConstants.Permissions.UPDATE_IMAGE_PERMISSION)]
        [HttpPut("{id}")]
        public IActionResult UpdateImage(Guid id, [FromBody] ChapterImage image)
        {
            // Logic để cập nhật ảnh
            return Ok(image);
        }

        [Authorize(Policy = CommonConstants.Permissions.DELETE_IMAGE_PERMISSION)]
        [HttpDelete("{id}")]
        public IActionResult DeleteImage(Guid id)
        {
            // Logic để xóa ảnh
            return NoContent();
        }
    }
}