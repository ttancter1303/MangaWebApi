using MangaWeb.Domain.Models.Files;
using Microsoft.AspNetCore.Http;

namespace MangaWeb.Domain.Abstractions.InfrastructureServices
{
    public interface IFileService
    {
        Task<FileInfoModel> UploadFile(IFormFile file, string folderName);
        void Delete(string imageUrl);
    }
}
