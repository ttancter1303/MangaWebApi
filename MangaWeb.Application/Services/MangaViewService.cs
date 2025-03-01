using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MangaWeb.Application.Services
{
    public interface IMangaViewService
    {
        Task RecordMangaViewAsync(Guid mangaId);
    }

    public class MangaViewService : IMangaViewService
    {
        private readonly DbContext _context;

        public MangaViewService(DbContext context)
        {
            _context = context;
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

            await _context.Set<MangaView>().AddAsync(mangaView);
            await _context.SaveChangesAsync();
        }
    }
}