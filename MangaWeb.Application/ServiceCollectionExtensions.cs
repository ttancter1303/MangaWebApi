using MangaWeb.Application.Services;
using MangaWeb.Domain.Abstractions.ApplicationServices;
using Microsoft.Extensions.DependencyInjection;

namespace MangaWeb.Application;

public static class ServiceCollectionExtensions
{
    public static void AddServicesApplication(this IServiceCollection services)
    {
        services.AddScoped<IChapterRepository, ChapterRepository>();
        services.AddScoped<IChapterService, ChapterService>();
        //services.AddScoped<IImageService, ImageService>();
        //services.AddScoped<IMangaService, MangaService>();
        services.AddScoped<IReviewMangaService, ReviewMangaService>();
        //services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<ITagService, TagService>();
        //services.AddScoped<IUserService, UserService>();
    }

}