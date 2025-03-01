using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Enums;
using MangaWeb.Domain.Models.Chapters;
using MangaWeb.Domain.Models.Mangas;
using MangaWeb.Domain.Models.Reviews;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaWeb.Application.Services
{
    public class MangaService : IMangaService
    {
        private readonly DbContext _context;

        public MangaService(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MangaViewModel>> GetAllMangasAsync()
        {
            return await _context.Set<Manga>()
                .Select(m => new MangaViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    CoverImageUrl = m.CoverImageUrl,
                    AuthorName = m.Author.Name,
                    Tags = m.Tags.Select(t => t.Name).ToList(),
                    Status = m.Status
                })
                .ToListAsync();
        }

        public async Task<MangaDetailViewModel> GetMangaByIdAsync(Guid id)
        {
            var manga = await _context.Set<Manga>()
                .Include(m => m.Chapters)
                .Include(m => m.ReviewMangas)
                .Include(m => m.Tags)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (manga == null) return null;

            return new MangaDetailViewModel
            {
                Id = manga.Id,
                Title = manga.Title,
                Description = manga.Description,
                CoverImageUrl = manga.CoverImageUrl,
                AuthorName = manga.Author.Name,
                Tags = manga.Tags.Select(t => t.Name).ToList(),
                Chapters = manga.Chapters.Select(c => new ChapterViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    ChapterNumber = c.ChapterNumber,
                }).ToList(),
                Reviews = manga.ReviewMangas.Select(r => new ReviewMangaViewModel
                {
                    // Assuming ReviewViewModel has properties like Id, Content, Rating, etc.
                }).ToList(),
                CreatedDate = manga.CreatedDate,
                UpdatedDate = manga.UpdatedDate,
                Status = manga.Status
            };
        }

        public async Task<Guid> CreateMangaAsync(MangaCreateViewModel model)
        {
            var manga = new Manga
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Description = model.Description,
                CoverImageUrl = model.CoverImageUrl,
                AuthorId = model.AuthorId,
                CreatedDate = DateTime.UtcNow,
                Status = EntityStatus.Active
            };

            await _context.Set<Manga>().AddAsync(manga);
            await _context.SaveChangesAsync();

            return manga.Id;
        }

        public async Task UpdateMangaAsync(MangaUpdateViewModel model)
        {
            var manga = await _context.Set<Manga>().FindAsync(model.Id);
            if (manga == null) return;

            manga.Title = model.Title;
            manga.Description = model.Description;
            manga.CoverImageUrl = model.CoverImageUrl;
            manga.AuthorId = model.AuthorId;
            manga.Tags = await _context.Set<Tag>().Where(t => model.TagIds.Contains(t.Id)).ToListAsync();
            manga.Status = model.Status;
            manga.UpdatedDate = DateTime.UtcNow;

            _context.Set<Manga>().Update(manga);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<MangaViewModel>> SearchMangasAsync(MangaSearchViewModel searchModel)
        {
            var query = _context.Set<Manga>().AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.Title))
            {
                query = query.Where(m => m.Title.Contains(searchModel.Title));
            }

            var mangas = await query
                .Select(m => new MangaViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    CoverImageUrl = m.CoverImageUrl,
                    AuthorName = m.Author.Name,
                    Tags = m.Tags.Select(t => t.Name).ToList(),
                    Status = m.Status
                })
                .ToListAsync();

            return mangas;
        }
        public async Task DeleteMangaAsync(Guid id)
        {
            var manga = await _context.Set<Manga>().FindAsync(id);
            if (manga == null) return;

            _context.Set<Manga>().Remove(manga);
            await _context.SaveChangesAsync();
        }
    }
}