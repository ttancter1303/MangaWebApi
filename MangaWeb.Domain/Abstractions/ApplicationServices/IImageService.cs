using MangaWeb.Domain.Models.Commons;
using MangaWeb.Domain.Models.Images;
using MangaWeb.Domain.Models.Users;
using Microsoft.AspNetCore.Http;

namespace MangaWeb.Domain.Abstractions.ApplicationServices
{
    public interface IImageService
    {
        Task<PageResult<ImageViewModel>> GetImages(ImageSearchQuery query);
        Task<ResponseResult> UploadImages(UploadImageViewModel model, UserProfileModel? currentUser = null);

        Task<ResponseResult> UpdateImage(UpdateImageViewModel model, UserProfileModel? currentUser = null);
        Task<ResponseResult> DeleteImage(Guid imageId);

        Task<ResponseResult> UpdateStatus(UpdateStatusViewModel model);
    }
}
