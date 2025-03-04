using MangaWeb.Domain.Models.Chapters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MangaWeb.Application.Interfaces
{
    public interface IChapterService
    {
        Task<IEnumerable<ChapterViewModel>> GetAllChaptersAsync();
        Task<ChapterDetailViewModel> GetChapterByIdAsync(Guid id);
        Task<IEnumerable<ChapterViewModel>> GetChaptersByMangaIdAsync(Guid mangaId);
        Task<ChapterViewModel> CreateChapterAsync(CreateChapterRequest request);
        Task<ChapterViewModel> UpdateChapterAsync(UpdateChapterRequest request);
        Task DeleteChapterAsync(Guid id);
        Task<bool> ReorderChapterImagesAsync(Guid chapterId, Dictionary<int, int> reorderMap);
    }
}