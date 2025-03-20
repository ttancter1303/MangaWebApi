using MangaWeb.Domain.Abstractions;
using MangaWeb.Domain.Entities;
using MangaWeb.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaWeb.Application.Services
{
    public class ReviewMangaRepository : IReviewMangaRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewMangaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReviewManga>> GetAllAsync()
        {
            return await _context.ReviewMangas.ToListAsync();
        }

        public async Task<ReviewManga> GetByIdAsync(Guid id)
        {
            return await _context.ReviewMangas.FindAsync(id);
        }

        public async Task AddAsync(ReviewManga review)
        {
            await _context.ReviewMangas.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ReviewManga review)
        {
            _context.ReviewMangas.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ReviewManga review)
        {
            _context.ReviewMangas.Remove(review);
            await _context.SaveChangesAsync();
        }

        public Task<List<ReviewManga>> GetReviewsByMangaIdAsync(Guid mangaId)
        {
            throw new NotImplementedException();
        }
    }
}
