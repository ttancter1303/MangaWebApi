using AutoMapper;
using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Exceptions;
using MangaWeb.Domain.Models.Chapters;
using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Abstractions;
using MangaWeb.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaWeb.Application.Services
{
    public class ChapterService : IChapterService
    {
        private readonly IChapterRepository _chapterRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;

        public ChapterService(IChapterRepository chapterRepository, IUnitOfWork unitOfWork, IMapper mapper, IStorageService storageService)
        {
            _chapterRepository = chapterRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageService = storageService;
        }

        public async Task<IEnumerable<ChapterViewModel>> GetAllChaptersAsync()
        {
            var chapters = await _chapterRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ChapterViewModel>>(chapters);
        }

        public async Task<ChapterDetailViewModel> GetChapterByIdAsync(Guid id)
        {
            var chapter = await _chapterRepository.GetByIdWithDetailsAsync(id);
            if (chapter == null)
                throw new ChapterNotFoundException(id);

            return _mapper.Map<ChapterDetailViewModel>(chapter);
        }

        public async Task<IEnumerable<ChapterViewModel>> GetChaptersByMangaIdAsync(Guid mangaId)
        {
            var chapters = await _chapterRepository.GetChaptersByMangaIdAsync(mangaId);
            return _mapper.Map<IEnumerable<ChapterViewModel>>(chapters);
        }

        public async Task<ChapterViewModel> CreateChapterAsync(ChapterCreateViewModel model)
        {
            var chapter = _mapper.Map<Chapter>(model);
            chapter.Id = Guid.NewGuid();
            chapter.CreatedDate = DateTime.UtcNow;
            chapter.Status = EntityStatus.Active;

            chapter.ImagePaths = model.ImageUrls;

            await _chapterRepository.AddAsync(chapter);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ChapterViewModel>(chapter);
        }

        public async Task<ChapterViewModel> UpdateChapterAsync(ChapterUpdateViewModel model)
        {
            var chapter = await _chapterRepository.GetByIdAsync(model.Id);
            if (chapter == null)
                throw new ChapterNotFoundException(model.Id);

            _mapper.Map(model, chapter);
            chapter.UpdatedDate = DateTime.UtcNow;

            await _chapterRepository.UpdateAsync(chapter);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ChapterViewModel>(chapter);
        }

        public async Task DeleteChapterAsync(Guid id)
        {
            var chapter = await _chapterRepository.GetByIdAsync(id);
            if (chapter == null)
                throw new ChapterNotFoundException(id);

            await _chapterRepository.DeleteAsync(chapter);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> ReorderChapterImagesAsync(Guid chapterId, Dictionary<int, int> reorderMap)
        {
            var chapter = await _chapterRepository.GetByIdAsync(chapterId);
            if (chapter == null)
                throw new ChapterNotFoundException(chapterId);

            var currentImages = chapter.ImagePaths.ToList();
            var orderedImages = new string[currentImages.Count];

            foreach (var reorder in reorderMap)
            {
                if (reorder.Key < currentImages.Count && reorder.Value < orderedImages.Length)
                {
                    orderedImages[reorder.Value] = currentImages[reorder.Key];
                }
                else
                {
                    throw new InvalidOperationException($"Invalid index mapping: Key = {reorder.Key}, Value = {reorder.Value}");
                }
            }

            chapter.ImagePaths = orderedImages.Where(x => x != null).ToList();

            await _chapterRepository.UpdateAsync(chapter);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
