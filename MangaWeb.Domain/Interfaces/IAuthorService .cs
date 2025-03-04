using MangaWeb.Domain.Models.Authors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MangaWeb.Domain.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorViewModel>> GetAllAuthorsAsync();
        Task<AuthorDetailViewModel> GetAuthorByIdAsync(Guid id);
        Task<Guid> CreateAuthorAsync(AuthorCreateViewModel model);
        Task UpdateAuthorAsync(AuthorUpdateViewModel model);
        Task DeleteAuthorAsync(Guid id);
        Task<IEnumerable<AuthorViewModel>> SearchAuthorsAsync(AuthorSearchViewModel searchModel);
    }
}