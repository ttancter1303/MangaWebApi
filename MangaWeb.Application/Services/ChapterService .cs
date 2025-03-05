using AutoMapper;
using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Exceptions; // Import các exception
using MangaWeb.Domain.Models.Chapters;
using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Abstractions;
using MangaWeb.Domain.Enums;


namespace MangaWeb.Application.Services
{
    public class ChapterService : IChapterService
    {
        private readonly IChapterRepository _chapterRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService; // Thêm IStorageService

        public ChapterService(IChapterRepository chapterRepository, IUnitOfWork unitOfWork, IMapper mapper, IStorageService storageService)
        {
            _chapterRepository = chapterRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageService = storageService; // Khởi tạo IStorageService
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
            {
                throw new ChapterNotFoundException(id.GetHashCode()); 
            }
            return _mapper.Map<ChapterDetailViewModel>(chapter);
        }

        public async Task<IEnumerable<ChapterViewModel>> GetChaptersByMangaIdAsync(Guid mangaId)
        {
            var chapters = await _chapterRepository.GetChaptersByMangaIdAsync(mangaId);
            return _mapper.Map<IEnumerable<ChapterViewModel>>(chapters);
        }

        public async Task<ChapterViewModel> CreateChapterAsync(CreateChapterRequest request)
        {
            var chapter = _mapper.Map<Chapter>(request);
            chapter.Id = Guid.NewGuid();
            chapter.CreatedDate = DateTime.UtcNow;
            chapter.Status = EntityStatus.Active;

            // Upload images using IStorageService
            var imagePaths = await _storageService.UploadFilesAsync(request.Images, $"chapters/{chapter.MangaId}/{chapter.ChapterNumber}");
            chapter.Images = imagePaths.ToArray();

            await _chapterRepository.AddAsync(chapter);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ChapterViewModel>(chapter);
        }

        public async Task<ChapterViewModel> UpdateChapterAsync(UpdateChapterRequest request)
        {
            var chapter = await _chapterRepository.GetByIdAsync(request.Id);
            if (chapter == null)
            {
                throw new ChapterNotFoundException(request.Id.GetHashCode()); // Sử dụng ChapterNotFoundException
            }

            _mapper.Map(request, chapter);
            chapter.UpdatedDate = DateTime.UtcNow;

            await _chapterRepository.UpdateAsync(chapter);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ChapterViewModel>(chapter);
        }

        public async Task DeleteChapterAsync(Guid id)
        {
            var chapter = await _chapterRepository.GetByIdAsync(id);
            if (chapter == null)
            {
                throw new ChapterNotFoundException(id.GetHashCode()); // Sử dụng ChapterNotFoundException
            }

            await _chapterRepository.DeleteAsync(chapter);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> ReorderChapterImagesAsync(Guid chapterId, Dictionary<int, int> reorderMap)
        {
            var chapter = await _chapterRepository.GetByIdAsync(chapterId);
            if (chapter == null)
            {
                throw new ChapterNotFoundException(chapterId.GetHashCode()); // Sử dụng ChapterNotFoundException
            }

            var currentImages = chapter.Images.ToList();
            var orderedImages = new string[currentImages.Count];

            // Sắp xếp lại hình ảnh dựa trên reorderMap
            foreach (var reorder in reorderMap)
            {
                // Kiểm tra chỉ số hợp lệ
                if (reorder.Key < currentImages.Count && reorder.Value < orderedImages.Length)
                {
                    orderedImages[reorder.Value] = currentImages[reorder.Key];
                }
                else
                {
                    throw new InvalidOperationException($"Chỉ số không hợp lệ trong bản đồ sắp xếp: Key = {reorder.Key}, Value = {reorder.Value}"); // Sử dụng InvalidOperationException
                }
            }

            // Lọc các hình ảnh không null và cập nhật lại
            chapter.Images = orderedImages.Where(x => x != null).ToArray();

            // Cập nhật chapter trong repository
            await _chapterRepository.UpdateAsync(chapter);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}