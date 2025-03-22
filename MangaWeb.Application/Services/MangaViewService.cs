using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using MangaWeb.Persistence; // Thêm namespace này

namespace MangaWeb.Application.Services
{
    public interface IMangaViewService
    {
        Task RecordMangaViewAsync(Guid mangaId);
    }

    public class MangaViewService : IMangaViewService
    {
        private readonly ApplicationDbContext _context;

        public MangaViewService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task RecordMangaViewAsync(Guid mangaId)
        {
            var mangaView = new MangaView
            {
                Id = Guid.NewGuid(),
                MangaId = mangaId,
                ViewDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                Status = EntityStatus.Active
            };

            await _context.MangaViews.AddAsync(mangaView);
            await _context.SaveChangesAsync();
        }
    }
}
