using Microsoft.AspNetCore.Http;

namespace MangaWeb.Domain.Interfaces
{
    public interface IStorageService
    {
        string StorageLocation { get; }
        Task<List<string>> UploadFilesAsync(List<IFormFile> files, string folderPath);
        Task<string> UploadFileAsync(IFormFile file, string folderPath);
        Task DeleteFileAsync(string filePath);
        Task DeleteFilesAsync(IEnumerable<string> filePaths);
        Task<bool> ExistsAsync(string filePath);
        Task<long> GetFileSizeAsync(string filePath);
    }
}