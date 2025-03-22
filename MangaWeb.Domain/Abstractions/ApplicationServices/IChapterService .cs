using MangaWeb.Domain.Models.Chapters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MangaWeb.Domain.Abstractions.ApplicationServices
{
    public interface IChapterService
    {
        Task<IEnumerable<ChapterViewModel>> GetAllChaptersAsync();
        Task<ChapterDetailViewModel> GetChapterByIdAsync(Guid id);
        Task<IEnumerable<ChapterViewModel>> GetChaptersByMangaIdAsync(Guid mangaId);
        Task<ChapterViewModel> CreateChapterAsync(ChapterCreateViewModel model);
        Task<ChapterViewModel> UpdateChapterAsync(ChapterUpdateViewModel model);
        Task DeleteChapterAsync(Guid id);
        Task<bool> ReorderChapterImagesAsync(Guid chapterId, Dictionary<int, int> reorderMap);
    }
}