
using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Enums;
using MangaWeb.Domain.Models.Mangas;
using MangaWeb.Domain.Models.Tags;
using MangaWeb.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaWeb.Application.Services
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _context;

        public TagService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TagViewModel>> GetAllTagsAsync()
        {
            return await _context.Tags
                .Select(t => new TagViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Status = t.Status
                })
                .ToListAsync();
        }

        public async Task<TagDetailViewModel> GetTagByIdAsync(Guid id)
        {
            var tag = await _context.Tags
                .Include(t => t.Mangas)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tag == null) return null;

            return new TagDetailViewModel
            {
                Id = tag.Id,
                Name = tag.Name,
                Mangas = tag.Mangas.Select(m => new MangaViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    CoverImageUrl = m.CoverImageUrl,
                    Status = m.Status
                }).ToList(),
                CreatedDate = tag.CreatedDate,
                UpdatedDate = tag.UpdatedDate,
                Status = tag.Status
            };
        }

        public async Task<Guid> CreateTagAsync(TagCreateViewModel model)
        {
            var tag = new Tag
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                CreatedDate = DateTime.UtcNow,
                Status = EntityStatus.Active
            };

            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return tag.Id;
        }

        public async Task UpdateTagAsync(TagUpdateViewModel model)
        {
            var tag = await _context.Tags.FindAsync(model.Id);
            if (tag == null) return;

            tag.Name = model.Name;
            tag.Status = model.Status;
            tag.UpdatedDate = DateTime.UtcNow;

            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTagAsync(Guid id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null) return;

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TagViewModel>> SearchTagsAsync(TagSearchViewModel searchModel)
        {
            var query = _context.Tags.AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                query = query.Where(t => t.Name.Contains(searchModel.Name));
            }

            return await query
                .Select(t => new TagViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Status = t.Status
                })
                .ToListAsync();
        }
    }
}