﻿using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Abstractions.InfrastructureServices;
using MangaWeb.Domain.Abstractions;
using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Enums;
using MangaWeb.Domain.Exceptions;
using MangaWeb.Domain.Models.Commons;
using MangaWeb.Domain.Models.Images;
using MangaWeb.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaWeb.Application.Services
{
    internal class ImageService : IImageService
    {
        private readonly IGenericRepository<GeneralImage, Guid> _imageRepository;
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ImageService> _logger;
        private const string _imageFolder = "generalImages";
        public ImageService(IGenericRepository<GeneralImage, Guid> imageRepository, IUnitOfWork unitOfWork, IFileService fileService, ILogger<ImageService> logger)
        {
            _imageRepository = imageRepository;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _logger = logger;
        }
        public async Task<PageResult<ImageViewModel>> GetImages(ImageSearchQuery query)
        {
            var result = new PageResult<ImageViewModel>
            {
                CurrentPage = query.PageIndex
            };

            var imageQuery = _imageRepository.FindAll();
            if (query.DisplayActiveItem)
            {
                imageQuery = imageQuery.Where(s => s.Status == EntityStatus.Active);
            }
            if (!string.IsNullOrEmpty(query.Keyword))
            {
                imageQuery = imageQuery.Where(s => s.Name.Contains(query.Keyword));

            }
            result.TotalCount = await imageQuery.CountAsync();
            var images = await imageQuery
                .OrderBy(s => s.Name)
                .Skip(query.SkipNo)
                .Take(query.TakeNo)
                .Select(s => new ImageViewModel()
                {
                    ImageId = s.Id,
                    ImageName = s.Name,
                    ImageUrl = s.Url
                }).ToListAsync();
            result.Data = images;
            return result;
        }

        public async Task<ResponseResult> UploadImages(UploadImageViewModel model, UserProfileModel? currentUser = null)
        {
            //foreach (var item in model.Images)
            //{
            //   await _fileService.UploadFile(item, _imageFolder);
            //}

            var uploadTask = model.Images.Select(s => _fileService.UploadFile(s, _imageFolder)).ToList();
            var uploadImages = await Task.WhenAll(uploadTask);
            foreach (var image in uploadImages)
            {
                _logger.LogInformation($"Uploaded file: {image.FilePath}");
            }
            if (uploadImages.Any())
            {
                var items = uploadImages.Select(s => new GeneralImage()
                {
                    Id = Guid.NewGuid(),
                    Name = s.FileName,
                    Url = s.FilePath,
                    CreatedBy = currentUser?.UserId
                }).ToList();
                try
                {
                    if (items == null || !items.Any())
                    {
                        _logger.LogWarning("No images to add.");
                    }
                    _imageRepository.AddRange(items);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError($"Database Error: {e.Message}");
                    throw new ImageException.UploadImageException();
                }

            }
            throw new ImageException.UploadImageException();
        }

        public async Task<ResponseResult> UpdateImage(UpdateImageViewModel model, UserProfileModel currentUser)
        {
            var image = await _imageRepository.FindByIdAsync(model.ImageId);
            if (image == null)
            {
                throw new ImageException.ImageNotFoundException(model.ImageId);
            }

            image.Name = model.ImageName;
            image.UpdatedBy = currentUser.UserId;
            image.UpdatedDate = DateTime.Now;
            try
            {
                _imageRepository.Update(image);
                await _unitOfWork.SaveChangesAsync();
                return ResponseResult.Success("update image successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                throw new ImageException.UpdateImageException(model.ImageId);
            }
        }

        public async Task<ResponseResult> DeleteImage(Guid imageId)
        {
            var image = await _imageRepository.FindByIdAsync(imageId);
            if (image == null)
            {
                throw new ImageException.ImageNotFoundException(imageId);
            }

            try
            {
                _imageRepository.Remove(image);
                await _unitOfWork.SaveChangesAsync();
                _fileService.Delete(image.Url);
                return ResponseResult.Success();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                throw new ImageException.DeleteImageException(imageId);
            }
        }

        public async Task<ResponseResult> UpdateStatus(UpdateStatusViewModel model)
        {
            var item = await _imageRepository.FindByIdAsync(model.Id);
            if (item == null)
            {
                throw new CategoryException.CategoryNotFoundException(model.Id);
            }
            item.Status = model.Status;
            try
            {
                _imageRepository.Update(item);
                await _unitOfWork.SaveChangesAsync();
                return ResponseResult.Success();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                throw new ImageException.UpdateImageException(model.Id);
            }

        }
    }
}
