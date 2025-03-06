using MangaWeb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaWeb.Domain.Abstractions
{
    public interface IReviewMangaRepository
    {
        Task<IEnumerable<ReviewManga>> GetAllAsync();
        Task<ReviewManga> GetByIdAsync(Guid id);
        Task AddAsync(ReviewManga review);
        Task UpdateAsync(ReviewManga review);
        Task DeleteAsync(ReviewManga review);
    }
}
