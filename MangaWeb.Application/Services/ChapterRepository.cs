using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaWeb.Application.Services
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly ApplicationDbContext _context;

        public ChapterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Chapter>> GetAllAsync()
        {
            return await _context.Chapters.ToListAsync();
        }

        public async Task<Chapter> GetByIdAsync(Guid id)
        {
            return await _context.Chapters.FindAsync(id);
        }

        public async Task<Chapter> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.Chapters
                .Include(c => c.Manga) 
                .FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task<IEnumerable<Chapter>> GetChaptersByMangaIdAsync(Guid mangaId)
        {
            return await _context.Chapters
                .Where(c => c.MangaId == mangaId)
                .ToListAsync();
        }

        public async Task AddAsync(Chapter chapter)
        {
            await _context.Chapters.AddAsync(chapter);
            await _context.SaveChangesAsync(); // Lưu thay đổi ngay lập tức
        }

        public async Task UpdateAsync(Chapter chapter)
        {
            _context.Chapters.Update(chapter);
            await _context.SaveChangesAsync(); // Lưu thay đổi ngay lập tức
        }

        public async Task DeleteAsync(Chapter chapter)
        {
            _context.Chapters.Remove(chapter);
            await _context.SaveChangesAsync(); // Lưu thay đổi ngay lập tức
        }
    }
}