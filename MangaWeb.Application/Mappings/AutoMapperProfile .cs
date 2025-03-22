using AutoMapper;
using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Models.Mangas;
using MangaWeb.Domain.Models.Reviews;
using MangaWeb.Domain.Models.Chapters;

namespace MangaWeb.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapping giữa MangaCreateViewModel và Manga Entity
            CreateMap<MangaCreateViewModel, Manga>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID sẽ được tạo tự động
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // Set ở Service
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore()) // Set ở Service
                .ForMember(dest => dest.Status, opt => opt.Ignore()); // Set mặc định Active

            // Mapping giữa MangaUpdateViewModel và Manga Entity
            CreateMap<MangaUpdateViewModel, Manga>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore()); // Gán trong Service

            // Mapping giữa Manga Entity và ViewModel
            CreateMap<Manga, MangaViewModel>();
            CreateMap<Manga, MangaDetailViewModel>();

            // Mapping giữa ReviewManga Entity và ReviewMangaDetailViewModel
            CreateMap<ReviewManga, ReviewMangaDetailViewModel>()
                .ForMember(dest => dest.MangaTitle, opt => opt.MapFrom(src => src.Manga.Title));

            // Mapping giữa ReviewManga Entity và ReviewMangaViewModel
            CreateMap<ReviewManga, ReviewMangaViewModel>();

            // 📌 Thêm Mapping cho Chapter
            // Mapping từ ChapterCreateViewModel -> Chapter Entity
            CreateMap<ChapterCreateViewModel, Chapter>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID sẽ được tạo trong Service
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // Gán trong Service
                .ForMember(dest => dest.Status, opt => opt.Ignore()) // Gán mặc định Active
                .ForMember(dest => dest.ImagePaths, opt => opt.MapFrom(src => src.ImageUrls));

            // Mapping từ Chapter Entity -> ChapterViewModel
            CreateMap<Chapter, ChapterViewModel>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ImagePaths))
                .ForMember(dest => dest.MangaTitle, opt => opt.MapFrom(src => src.Manga.Title));
        }
    }
}
