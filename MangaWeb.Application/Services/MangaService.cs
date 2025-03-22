using AutoMapper;
using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Abstractions;
using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Enums;
using MangaWeb.Domain.Exceptions;
using MangaWeb.Domain.Models.Mangas;
using MangaWeb.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;


namespace MangaWeb.Application.Services
{
    public class MangaService : IMangaService
    {
        private readonly IMangaRepository _mangaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<GeneralImage, Guid> _generalImageRepository;
        private readonly IMapper _mapper;

        public MangaService(IMangaRepository mangaRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mangaRepository = mangaRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MangaViewModel>> GetAllMangasAsync()
        {
            var mangas = await _mangaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MangaViewModel>>(mangas);
        }

        public async Task<MangaDetailViewModel> GetMangaByIdAsync(Guid id)
        {
            var manga = await _mangaRepository.GetByIdWithDetailsAsync(id);
            if (manga == null)
            {
                throw new MangaNotFoundException(id);
            }
            return _mapper.Map<MangaDetailViewModel>(manga);
        }
        public async Task<Guid> CreateMangaAsync(MangaCreateViewModel model)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var manga = _mapper.Map<Manga>(model);
                manga.Id = Guid.NewGuid();
                manga.CreatedDate = DateTime.UtcNow;
                manga.Status = EntityStatus.Active;

                manga.CoverImageUrl = model.CoverImageUrl;

                await _mangaRepository.AddAsync(manga);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return manga.Id;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                throw new MangaException.CreateMangaException(model.Title);
            }
        }



        public async Task UpdateMangaAsync(MangaUpdateViewModel model)
        {
            var manga = await _mangaRepository.GetByIdAsync(model.Id);
            if (manga == null)
            {
                throw new MangaNotFoundException(model.Id);
            }

            _mapper.Map(model, manga);
            manga.UpdatedDate = DateTime.UtcNow;

            await _mangaRepository.UpdateAsync(manga);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteMangaAsync(Guid id)
        {
            var manga = await _mangaRepository.GetByIdAsync(id);
            if (manga == null)
            {
                throw new MangaNotFoundException(id);
            }

            await _mangaRepository.DeleteAsync(manga);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<MangaViewModel>> SearchMangasAsync(MangaSearchViewModel searchModel)
        {
            var mangas = await _mangaRepository.SearchAsync(searchModel.Title);
            return _mapper.Map<IEnumerable<MangaViewModel>>(mangas);
        }


    }
}