using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Models.Reviews;
using MangaWeb.Persistence; // Đảm bảo bạn có namespace đúng
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MangaWeb.Application.Services
{
    public class ReviewMangaService : IReviewMangaService
    {
        private readonly ApplicationDbContext _context; // Sử dụng DbContext trực tiếp

        public ReviewMangaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReviewMangaViewModel>> GetAllReviewsAsync()
        {
            var reviews = await _context.ReviewMangas.ToListAsync(); // Lấy tất cả đánh giá
            return MapToViewModel(reviews);
        }

        public async Task<ReviewMangaViewModel> GetReviewByIdAsync(Guid id)
        {
            var review = await _context.ReviewMangas.FindAsync(id); // Tìm đánh giá theo ID
            return MapToViewModel(review);
        }

        public async Task<IEnumerable<ReviewMangaViewModel>> GetReviewsByMangaIdAsync(Guid mangaId)
        {
            var reviews = await _context.ReviewMangas
                .Where(r => r.MangaId == mangaId)
                .ToListAsync(); // Lấy đánh giá theo MangaId
            return MapToViewModel(reviews);
        }

        public async Task<ReviewMangaViewModel> CreateReviewAsync(ReviewMangaViewModel review)
        {
            var reviewEntity = MapToEntity(review);
            await _context.ReviewMangas.AddAsync(reviewEntity); // Thêm đánh giá mới
            await _context.SaveChangesAsync(); // Lưu thay đổi
            return MapToViewModel(reviewEntity);
        }

        public async Task UpdateReviewAsync(ReviewMangaViewModel review)
        {
            var reviewEntity = MapToEntity(review);
            _context.ReviewMangas.Update(reviewEntity); // Cập nhật đánh giá
            await _context.SaveChangesAsync(); // Lưu thay đổi
        }

        public async Task DeleteReviewAsync(Guid id)
        {
            var review = await _context.ReviewMangas.FindAsync(id); // Tìm đánh giá theo ID
            if (review != null)
            {
                _context.ReviewMangas.Remove(review); // Xóa đánh giá
                await _context.SaveChangesAsync(); // Lưu thay đổi
            }
        }

        // Các phương thức ánh xạ giữa Entity và ViewModel
        private ReviewManga MapToEntity(ReviewMangaViewModel reviewViewModel)
        {
            return new ReviewManga
            {
                Id = reviewViewModel.Id,
                Title = reviewViewModel.Title,
                Content = reviewViewModel.Content,
                Rating = reviewViewModel.Rating,
                MangaId = reviewViewModel.MangaId,
                CreatedDate = DateTime.UtcNow,
                // Các thuộc tính khác nếu cần
            };
        }

        private ReviewMangaViewModel MapToViewModel(ReviewManga review)
        {
            return new ReviewMangaViewModel
            {
                Id = review.Id,
                Title = review.Title,
                Content = review.Content,
                Rating = review.Rating,
                MangaId = review.MangaId,
                // Các thuộc tính khác nếu cần
            };
        }

        private IEnumerable<ReviewMangaViewModel> MapToViewModel(IEnumerable<ReviewManga> reviews)
        {
            var reviewViewModels = new List<ReviewMangaViewModel>();
            foreach (var review in reviews)
            {
                reviewViewModels.Add(MapToViewModel(review));
            }
            return reviewViewModels;
        }
    }
}