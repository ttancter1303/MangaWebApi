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
    public class MangaRepository : IMangaRepository
    {
        private readonly ApplicationDbContext _context;

        public MangaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Manga>> GetAllAsync()
        {
            return await _context.Mangas.ToListAsync();
        }

        public async Task<Manga> GetByIdAsync(Guid id)
        {
            return await _context.Mangas.FindAsync(id);
        }

        public async Task<Manga> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.Mangas
                .Include(m => m.Author)
                .Include(m => m.Tags)
                .Include(m => m.Chapters)
                .Include(m => m.ReviewMangas)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Manga manga)
        {
            await _context.Mangas.AddAsync(manga);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Manga manga)
        {
            _context.Mangas.Update(manga);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Manga manga)
        {
            _context.Mangas.Remove(manga);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Manga>> SearchAsync(string title)
        {
            return await _context.Mangas
                .Where(m => m.Title.Contains(title))
                .ToListAsync();
        }
    }
}
