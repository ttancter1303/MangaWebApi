using MangaWeb.Domain.Models.Reviews;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MangaWeb.Domain.Abstractions.ApplicationServices
{
    public interface IReviewMangaService
    {
        Task<IEnumerable<ReviewMangaDetailViewModel>> GetAllReviewsAsync();
        Task<ReviewMangaDetailViewModel> GetReviewByIdAsync(Guid id);
        Task<IEnumerable<ReviewMangaDetailViewModel>> GetReviewsByMangaIdAsync(Guid mangaId);
        Task<ReviewMangaDetailViewModel> CreateReviewAsync(ReviewMangaCreateViewModel review);
        Task UpdateReviewAsync(ReviewMangaUpdateViewModel review);
        Task DeleteReviewAsync(Guid id);
    }
}
