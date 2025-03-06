using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Models.Reviews;
using MangaWeb.Domain.Abstractions;
using MangaWeb.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MangaWeb.Application.Services
{
    public class ReviewMangaService : IReviewMangaService
    {
        private readonly IReviewMangaRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewMangaService(IReviewMangaRepository reviewRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewMangaViewModel>> GetAllReviewsAsync()
        {
            var reviews = await _reviewRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReviewMangaViewModel>>(reviews);
        }

        public async Task<ReviewMangaViewModel> GetReviewByIdAsync(Guid id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null) throw new Exception("Review not found");
            return _mapper.Map<ReviewMangaViewModel>(review);
        }

        public async Task<ReviewMangaViewModel> CreateReviewAsync(ReviewMangaCreateViewModel reviewModel)
        {
            var review = _mapper.Map<ReviewManga>(reviewModel);
            await _reviewRepository.AddAsync(review);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ReviewMangaViewModel>(review);
        }

        public async Task UpdateReviewAsync(ReviewMangaUpdateViewModel reviewModel)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewModel.Id);
            if (review == null) throw new Exception("Review not found");

            _mapper.Map(reviewModel, review);
            await _reviewRepository.UpdateAsync(review);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(Guid id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null) throw new Exception("Review not found");

            await _reviewRepository.DeleteAsync(review);
            await _unitOfWork.SaveChangesAsync();
        }

        Task<IEnumerable<ReviewMangaDetailViewModel>> IReviewMangaService.GetAllReviewsAsync()
        {
            throw new NotImplementedException();
        }

        Task<ReviewMangaDetailViewModel> IReviewMangaService.GetReviewByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReviewMangaDetailViewModel>> GetReviewsByMangaIdAsync(Guid mangaId)
        {
            throw new NotImplementedException();
        }

        Task<ReviewMangaDetailViewModel> IReviewMangaService.CreateReviewAsync(ReviewMangaCreateViewModel review)
        {
            throw new NotImplementedException();
        }
    }
}
