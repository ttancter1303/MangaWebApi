using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Enums;
using MangaWeb.Domain.Models.Chapters;
using MangaWeb.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaWeb.Application.Services
{
    public class ChapterService : IChapterService
    {
        private readonly ApplicationDbContext _context;

        public ChapterService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChapterViewModel>> GetAllChaptersAsync()
        {
            return await _context.Chapters
                .Select(c => new ChapterViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    ChapterNumber = c.ChapterNumber,
                    MangaId = c.MangaId,
                    ChapterImages = c.ChapterImages.Select(ci => ci.ImageUrl).ToList(),
                    Status = c.Status
                })
                .ToListAsync();
        }

        public async Task<ChapterDetailViewModel> GetChapterByIdAsync(Guid id)
        {
            var chapter = await _context.Chapters
                .Include(c => c.ChapterImages)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (chapter == null) return null;

            return new ChapterDetailViewModel
            {
                Id = chapter.Id,
                Title = chapter.Title,
                ChapterNumber = chapter.ChapterNumber,
                MangaId = chapter.MangaId,
                MangaTitle = chapter.Manga.Title,
                ChapterImages = chapter.ChapterImages.OrderBy(ci => ci.Order).Select(ci => ci.ImageUrl).ToList(),
                CreatedDate = chapter.CreatedDate,
                UpdatedDate = chapter.UpdatedDate,
                Status = chapter.Status
            };
        }

        public async Task<Guid> CreateChapterAsync(ChapterCreateViewModel model)
        {
            var chapter = new Chapter
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                ChapterNumber = model.ChapterNumber,
                MangaId = model.MangaId,
                CreatedDate = DateTime.UtcNow,
                Status = EntityStatus.Active
            };

            await _context.Chapters.AddAsync(chapter);
            await _context.SaveChangesAsync();

            return chapter.Id;
        }

        public async Task UpdateChapterAsync(ChapterUpdateViewModel model)
        {
            var chapter = await _context.Chapters.FindAsync(model.Id);
            if (chapter == null) return;

            chapter.Title = model.Title;
            chapter.ChapterNumber = model.ChapterNumber;
            chapter.Status = model.Status;
            chapter.UpdatedDate = DateTime.UtcNow;

            _context.Chapters.Update(chapter);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChapterAsync(Guid id)
        {
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter == null) return;

            _context.Chapters.Remove(chapter);
            await _context.SaveChangesAsync();
        }
    }
}