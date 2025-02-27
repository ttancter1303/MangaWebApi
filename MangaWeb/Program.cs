using Microsoft.AspNetCore.Identity;
using MangaWeb.Persistence;
using MangaWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using MangaWeb.Domain.Utility;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký Identity
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Đăng ký các dịch vụ khác
// builder.Services.AddScoped<IViewService, ViewService>();
// builder.Services.AddScoped<IReviewService, ReviewService>();
// builder.Services.AddScoped<IImageService, ImageService>();

// Đăng ký controllers
builder.Services.AddControllers();

// Đăng ký Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MangaWeb API",
        Version = "v1",
        Description = "API for managing manga, chapters, and user interactions.",
        Contact = new OpenApiContact
        {
            Name = "Your Name",
            Email = "your.email@example.com",
            Url = new Uri("https://yourwebsite.com"),
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT"),
        }
    });
});

// Đăng ký các policy dựa trên permission
builder.Services.AddAuthorization(options =>
{
    // User Permissions
    options.AddPolicy(CommonConstants.Permissions.USER_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.USER_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.ADD_USER_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.ADD_USER_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.UPDATE_USER_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.UPDATE_USER_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.DELETE_USER_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.DELETE_USER_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.VIEW_USER_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.VIEW_USER_PERMISSION));

    // Role Permissions
    options.AddPolicy(CommonConstants.Permissions.ROLE_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.ROLE_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.ADD_ROLE_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.ADD_ROLE_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.UPDATE_ROLE_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.UPDATE_ROLE_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.DELETE_ROLE_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.DELETE_ROLE_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.VIEW_ROLE_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.VIEW_ROLE_PERMISSION));

    // Manga Permissions
    options.AddPolicy(CommonConstants.Permissions.MANGA_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.MANGA_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.ADD_MANGA_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.ADD_MANGA_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.UPDATE_MANGA_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.UPDATE_MANGA_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.DELETE_MANGA_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.DELETE_MANGA_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.VIEW_MANGA_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.VIEW_MANGA_PERMISSION));

    // Chapter Permissions
    options.AddPolicy(CommonConstants.Permissions.CHAPTER_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.CHAPTER_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.ADD_CHAPTER_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.ADD_CHAPTER_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.UPDATE_CHAPTER_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.UPDATE_CHAPTER_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.DELETE_CHAPTER_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.DELETE_CHAPTER_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.VIEW_CHAPTER_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.VIEW_CHAPTER_PERMISSION));

    // Author Permissions
    options.AddPolicy(CommonConstants.Permissions.AUTHOR_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.AUTHOR_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.ADD_AUTHOR_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.ADD_AUTHOR_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.UPDATE_AUTHOR_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.UPDATE_AUTHOR_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.DELETE_AUTHOR_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.DELETE_AUTHOR_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.VIEW_AUTHOR_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.VIEW_AUTHOR_PERMISSION));

    // Tag Permissions
    options.AddPolicy(CommonConstants.Permissions.TAG_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.TAG_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.ADD_TAG_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.ADD_TAG_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.UPDATE_TAG_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.UPDATE_TAG_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.DELETE_TAG_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.DELETE_TAG_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.VIEW_TAG_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.VIEW_TAG_PERMISSION));

    // Review Permissions
    options.AddPolicy(CommonConstants.Permissions.REVIEW_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.REVIEW_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.ADD_REVIEW_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.ADD_REVIEW_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.UPDATE_REVIEW_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.UPDATE_REVIEW_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.DELETE_REVIEW_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.DELETE_REVIEW_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.VIEW_REVIEW_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.VIEW_REVIEW_PERMISSION));

    // Image Permissions
    options.AddPolicy(CommonConstants.Permissions.IMAGE_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.IMAGE_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.ADD_IMAGE_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.ADD_IMAGE_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.UPDATE_IMAGE_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.UPDATE_IMAGE_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.DELETE_IMAGE_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.DELETE_IMAGE_PERMISSION));
    options.AddPolicy(CommonConstants.Permissions.VIEW_IMAGE_PERMISSION, policy =>
        policy.RequireClaim("Permission", CommonConstants.Permissions.VIEW_IMAGE_PERMISSION));
});

var app = builder.Build();

// Cấu hình middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MangaWeb API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();