using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Enums;
using MangaWeb.Domain.Models.Authors;
using MangaWeb.Domain.Models.Mangas;
using MangaWeb.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaWeb.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuthorViewModel>> GetAllAuthorsAsync()
        {
            return await _context.Authors
                .Select(a => new AuthorViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    ProfileImageUrl = a.ProfileImageUrl,
                    Status = a.Status
                })
                .ToListAsync();
        }

        public async Task<AuthorDetailViewModel> GetAuthorByIdAsync(Guid id)
        {
            var author = await _context.Authors
                .Include(a => a.Mangas)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null) return null;

            return new AuthorDetailViewModel
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio,
                ProfileImageUrl = author.ProfileImageUrl,
                Mangas = author.Mangas.Select(m => new MangaViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    CoverImageUrl = m.CoverImageUrl,
                    Status = m.Status
                }).ToList(),
                CreatedDate = author.CreatedDate,
                UpdatedDate = author.UpdatedDate,
                Status = author.Status
            };
        }

        public async Task<Guid> CreateAuthorAsync(AuthorCreateViewModel model)
        {
            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Bio = model.Bio,
                ProfileImageUrl = model.ProfileImageUrl,
                CreatedDate = DateTime.UtcNow,
                Status = EntityStatus.Active
            };

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return author.Id;
        }

        public async Task UpdateAuthorAsync(AuthorUpdateViewModel model)
        {
            var author = await _context.Authors.FindAsync(model.Id);
            if (author == null) return;

            author.Name = model.Name;
            author.Bio = model.Bio;
            author.ProfileImageUrl = model.ProfileImageUrl;
            author.Status = model.Status;
            author.UpdatedDate = DateTime.UtcNow;

            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(Guid id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuthorViewModel>> SearchAuthorsAsync(AuthorSearchViewModel searchModel)
        {
            var query = _context.Authors.AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                query = query.Where(a => a.Name.Contains(searchModel.Name));
            }

            return await query
                .Select(a => new AuthorViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    ProfileImageUrl = a.ProfileImageUrl,
                    Status = a.Status
                })
                .ToListAsync();
        }
    }
}