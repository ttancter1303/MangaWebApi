using MangaWeb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MangaWeb.Domain.Interfaces
{
    public interface IChapterRepository
    {
        Task<IEnumerable<Chapter>> GetAllAsync();
        Task<Chapter> GetByIdAsync(Guid id);
        Task<Chapter> GetByIdWithDetailsAsync(Guid id);
        Task<IEnumerable<Chapter>> GetChaptersByMangaIdAsync(Guid mangaId);
        Task AddAsync(Chapter chapter);
        Task UpdateAsync(Chapter chapter);
        Task DeleteAsync(Chapter chapter);
    }
}