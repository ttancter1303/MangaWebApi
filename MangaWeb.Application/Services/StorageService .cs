using MangaWeb.Domain.Abstractions.ApplicationServices;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
namespace MangaWeb.Application.Services
{
    public class StorageService : IStorageService
    {
        public string StorageLocation => throw new NotImplementedException();

        public Task DeleteFileAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFilesAsync(IEnumerable<string> filePaths)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetFileSizeAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadFileAsync(IFormFile file, string folderPath)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> UploadFilesAsync(List<IFormFile> files, string folderPath)
        {
            var uploadedPaths = new List<string>();

            foreach (var file in files)
            {
                var filePath = Path.Combine(folderPath, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                uploadedPaths.Add(filePath);
            }

            return uploadedPaths;
        }
    }
}
