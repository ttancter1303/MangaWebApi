using AutoMapper;
using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Abstractions;
using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Enums;
using MangaWeb.Domain.Exceptions;
using MangaWeb.Domain.Models.Mangas;
using MangaWeb.Domain.Abstractions.InfrastructureServices;
using MangaWeb.Domain.Models.Tags;
using MangaWeb.Domain.Models.Images;
using Microsoft.AspNetCore.Http;
using MangaWeb.Domain.Models.Commons;

namespace MangaWeb.Application.Services
{
    public class MangaService : IMangaService
    {
        private readonly IMangaRepository _mangaRepository;
        private readonly ITagService _tagService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public MangaService(
            IMangaRepository mangaRepository,
            ITagService tagService,
            IUnitOfWork unitOfWork,
            IFileService fileService,
            IMapper mapper,
            IImageService imageService)
        {
            _mangaRepository = mangaRepository;
            _tagService = tagService;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _mapper = mapper;
            _imageService = imageService;
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
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "MangaCreateViewModel cannot be null.");
            }

            // Lấy danh sách Tag từ `ITagService`
            var tagViewModels = await _tagService.GetAllTagsAsync();
            var tags = tagViewModels
                .Where(tag => model.TagIds.Contains(tag.Id))
                .Select(tag => _mapper.Map<Tag>(tag))
                .ToList();

            if (tags.Count != model.TagIds.Count)
            {
                throw new Exception("Some provided TagIds are invalid.");
            }

            var manga = _mapper.Map<Manga>(model);
            manga.Id = Guid.NewGuid();
            manga.CreatedDate = DateTime.UtcNow;
            manga.Status = EntityStatus.Active;
            manga.Tags = tags;

            // **Xử lý upload ảnh bằng ImageService**
            if (model.CoverImage != null)
            {
                var uploadResult = await _imageService.UploadImages(new UploadImageViewModel
                {
                    Images = new List<IFormFile> { model.CoverImage }
                });

                // Kiểm tra nếu upload thành công
                if (uploadResult.StatusCode == StatusCodes.Status200OK && uploadResult is ResponseResult<List<ImageViewModel>> imageResponse)
                {
                    var uploadedImages = imageResponse.Data;

                    if (uploadedImages != null && uploadedImages.Any())
                    {
                        manga.CoverImageUrl = uploadedImages.First().ImageUrl;
                    }
                    else
                    {
                        throw new Exception("Upload cover image failed: No image returned.");
                    }
                }
                else
                {
                    throw new Exception($"Upload cover image failed. Status Code: {uploadResult.StatusCode}, Message: {uploadResult.Message}");
                }
            }

            await _mangaRepository.AddAsync(manga);
            await _unitOfWork.SaveChangesAsync();

            return manga.Id;
        }



        public async Task UpdateMangaAsync(MangaUpdateViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "MangaUpdateViewModel cannot be null.");
            }

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

            // **Xóa ảnh nếu có**
            if (!string.IsNullOrEmpty(manga.CoverImageUrl))
            {
                await _imageService.DeleteImage(Guid.Parse(manga.CoverImageUrl));
            }

            await _mangaRepository.DeleteAsync(manga);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<MangaViewModel>> SearchMangasAsync(MangaSearchViewModel searchModel)
        {
            if (searchModel == null || string.IsNullOrWhiteSpace(searchModel.Title))
            {
                return Enumerable.Empty<MangaViewModel>();
            }

            var mangas = await _mangaRepository.SearchAsync(searchModel.Title);
            return _mapper.Map<IEnumerable<MangaViewModel>>(mangas);
        }
    }
}
