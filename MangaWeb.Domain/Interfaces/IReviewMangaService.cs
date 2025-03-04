using MangaWeb.Domain.Models.Reviews;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MangaWeb.Domain.Interfaces
{
    public interface IReviewMangaService
    {
        Task<IEnumerable<ReviewMangaViewModel>> GetAllReviewsAsync();
        Task<ReviewMangaViewModel> GetReviewByIdAsync(Guid id);
        Task<IEnumerable<ReviewMangaViewModel>> GetReviewsByMangaIdAsync(Guid mangaId);
        Task<ReviewMangaViewModel> CreateReviewAsync(ReviewMangaViewModel review);
        Task UpdateReviewAsync(ReviewMangaViewModel review);
        Task DeleteReviewAsync(Guid id);
    }
}