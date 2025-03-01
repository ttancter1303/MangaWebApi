using MangaWeb.Domain.Models.Tags;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MangaWeb.Application.Services
{
    public interface ITagService
    {
        Task<IEnumerable<TagViewModel>> GetAllTagsAsync();
        Task<TagDetailViewModel> GetTagByIdAsync(Guid id);
        Task<Guid> CreateTagAsync(TagCreateViewModel model);
        Task UpdateTagAsync(TagUpdateViewModel model);
        Task DeleteTagAsync(Guid id);
        Task<IEnumerable<TagViewModel>> SearchTagsAsync(TagSearchViewModel searchModel);
    }
}