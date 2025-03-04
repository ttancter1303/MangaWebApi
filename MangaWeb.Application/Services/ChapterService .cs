using AutoMapper;
using MangaWeb.Domain.Entities;

using MangaWeb.Domain.Models.Chapters;
using MangaWeb.Domain.Exceptions;
using MangaWeb.Domain.Interfaces;
using MangaWeb.Domain.Abstractions;

namespace MangaWeb.Application.Services
{
    public class ChapterService : IChapterService
    {
        private readonly IChapterRepository _chapterRepository;
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ChapterService(
            IChapterRepository chapterRepository,
            IStorageService storageService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _chapterRepository = chapterRepository;
            _storageService = storageService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
                throw new NotFoundException($"Không tìm thấy chapter với id: {id}");

            return _mapper.Map<ChapterDetailViewModel>(chapter);
        }

        public async Task<IEnumerable<ChapterViewModel>> GetChaptersByMangaIdAsync(Guid mangaId)
        {
            var chapters = await _chapterRepository.GetChaptersByMangaIdAsync(mangaId);
            return _mapper.Map<IEnumerable<ChapterViewModel>>(chapters);
        }

        public async Task<ChapterViewModel> CreateChapterAsync(CreateChapterRequest request)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Upload images
                var folderPath = $"chapters/{request.MangaId}/{request.ChapterNumber}";
                var imagePaths = await _storageService.UploadFilesAsync(request.Images, folderPath);

                var chapter = new Chapter
                {
                    Id = Guid.NewGuid(),
                    Title = request.Title,
                    ChapterNumber = request.ChapterNumber,
                    MangaId = request.MangaId,
                    Images = imagePaths.ToArray(),
                    PageCount = imagePaths.Count,
                    StorageLocation = _storageService.StorageLocation,
                    TotalSize = request.Images.Sum(f => f.Length),
                    CreatedDate = DateTime.UtcNow,
                    Status = Domain.Enums.EntityStatus.Active
                };

                await _chapterRepository.AddAsync(chapter);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<ChapterViewModel>(chapter);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<ChapterViewModel> UpdateChapterAsync(UpdateChapterRequest request)
        {
            var chapter = await _chapterRepository.GetByIdAsync(request.Id);
            if (chapter == null)
                throw new NotFoundException($"Không tìm thấy chapter với id: {request.Id}");

            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var currentImages = chapter.Images.ToList();

                // Remove images if specified
                if (request.RemoveImagePaths?.Any() == true)
                {
                    foreach (var path in request.RemoveImagePaths)
                    {
                        currentImages.Remove(path);
                        await _storageService.DeleteFileAsync(path);
                    }
                }

                // Upload new images if any
                if (request.NewImages?.Any() == true)
                {
                    var folderPath = $"chapters/{chapter.MangaId}/{request.ChapterNumber}";
                    var newImagePaths = await _storageService.UploadFilesAsync(request.NewImages, folderPath);
                    currentImages.AddRange(newImagePaths);
                }

                // Update chapter
                chapter.Title = request.Title;
                chapter.ChapterNumber = request.ChapterNumber;
                chapter.Images = currentImages.ToArray();
                chapter.PageCount = currentImages.Count;
                chapter.UpdatedDate = DateTime.UtcNow;

                // Reorder images if specified
                if (request.ReorderImages?.Any() == true)
                {
                    var orderedImages = new string[currentImages.Count];
                    foreach (var reorder in request.ReorderImages)
                    {
                        if (reorder.Key < currentImages.Count && reorder.Value < currentImages.Count)
                        {
                            orderedImages[reorder.Value] = currentImages[reorder.Key];
                        }
                    }
                    chapter.Images = orderedImages.Where(x => x != null).ToArray();
                }

                await _chapterRepository.UpdateAsync(chapter);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<ChapterViewModel>(chapter);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task DeleteChapterAsync(Guid id)
        {
            var chapter = await _chapterRepository.GetByIdAsync(id);
            if (chapter == null)
                throw new NotFoundException($"Không tìm thấy chapter với id: {id}");

            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Delete images from storage
                foreach (var imagePath in chapter.Images)
                {
                    await _storageService.DeleteFileAsync(imagePath);
                }

                await _chapterRepository.DeleteAsync(chapter);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<bool> ReorderChapterImagesAsync(Guid chapterId, Dictionary<int, int> reorderMap)
        {
            var chapter = await _chapterRepository.GetByIdAsync(chapterId);
            if (chapter == null)
                throw new NotFoundException($"Không tìm thấy chapter với id: {chapterId}");

            var currentImages = chapter.Images.ToList();
            var orderedImages = new string[currentImages.Count];

            foreach (var reorder in reorderMap)
            {
                if (reorder.Key < currentImages.Count && reorder.Value < currentImages.Count)
                {
                    orderedImages[reorder.Value] = currentImages[reorder.Key];
                }
            }

            chapter.Images = orderedImages.Where(x => x != null).ToArray();
            await _chapterRepository.UpdateAsync(chapter);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}