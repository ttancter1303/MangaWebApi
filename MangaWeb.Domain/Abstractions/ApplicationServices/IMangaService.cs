﻿using MangaWeb.Domain.Models.Mangas;
using MangaWeb.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MangaWeb.Domain.Abstractions.ApplicationServices
{
    public interface IMangaService
    {
        Task<IEnumerable<MangaViewModel>> GetAllMangasAsync();
        Task<MangaDetailViewModel> GetMangaByIdAsync(Guid id);
        Task<Guid> CreateMangaAsync(MangaCreateViewModel model); // Return Guid
        Task UpdateMangaAsync(MangaUpdateViewModel model);
        Task DeleteMangaAsync(Guid id);
        Task<IEnumerable<MangaViewModel>> SearchMangasAsync(MangaSearchViewModel searchModel);
    };
}