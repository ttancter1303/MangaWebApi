using MangaWeb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MangaWeb.Domain.Abstractions
{
    public interface IMangaRepository
    {
        Task<IEnumerable<Manga>> GetAllAsync();
        Task<Manga> GetByIdAsync(Guid id);
        Task<Manga?> GetByIdWithDetailsAsync(Guid id);
        Task AddAsync(Manga manga);
        Task UpdateAsync(Manga manga);
        Task DeleteAsync(Manga manga);
        Task<IEnumerable<Manga>> SearchAsync(string title);
    }
}
