using MangaWeb.Domain.Models.Chapters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MangaWeb.Application.Services
{
    public interface IChapterService
    {
        Task<IEnumerable<ChapterViewModel>> GetAllChaptersAsync();
        Task<ChapterDetailViewModel> GetChapterByIdAsync(Guid id);
        Task<Guid> CreateChapterAsync(ChapterCreateViewModel model);
        Task UpdateChapterAsync(ChapterUpdateViewModel model);
        Task DeleteChapterAsync(Guid id);
    }
}