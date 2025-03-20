using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Models.Reviews;
using MangaWeb.Domain.Abstractions;
using MangaWeb.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MangaWeb.Domain.Exceptions;

namespace MangaWeb.Application.Services
{
    public class ReviewMangaService : IReviewMangaService
    {
        private readonly IReviewMangaRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ReviewMangaService(
            IReviewMangaRepository reviewRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserService userService)  
        {
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;  // ✅ Gán _userService
        }

        public async Task<IEnumerable<ReviewMangaViewModel>> GetAllReviewsAsync()
        {
            var reviews = await _reviewRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReviewMangaViewModel>>(reviews);
        }

        public async Task<ReviewMangaDetailViewModel> GetReviewByIdAsync(Guid id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null)
            {
                throw new ReviewNotFoundException(id);
            }

            var reviewDetail = _mapper.Map<ReviewMangaDetailViewModel>(review);
            reviewDetail.UserName = await _userService.GetUserNameByIdAsync(review.UserId);  

            return reviewDetail;
        }

        public async Task<IEnumerable<ReviewMangaDetailViewModel>> GetReviewsByMangaIdAsync(Guid mangaId)
        {
            var reviews = await _reviewRepository.GetReviewsByMangaIdAsync(mangaId);
            return _mapper.Map<IEnumerable<ReviewMangaDetailViewModel>>(reviews);
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
            if (review == null) throw new ReviewNotFoundException(reviewModel.Id);  

            _mapper.Map(reviewModel, review);
            await _reviewRepository.UpdateAsync(review);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(Guid id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null) throw new ReviewNotFoundException(id);  

            await _reviewRepository.DeleteAsync(review);
            await _unitOfWork.SaveChangesAsync();
        }

        Task<IEnumerable<ReviewMangaDetailViewModel>> IReviewMangaService.GetAllReviewsAsync()
        {
            throw new NotImplementedException();
        }

        Task<ReviewMangaDetailViewModel> IReviewMangaService.CreateReviewAsync(ReviewMangaCreateViewModel review)
        {
            throw new NotImplementedException();
        }
    }
}
