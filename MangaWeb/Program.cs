using Microsoft.AspNetCore.Identity;
using MangaWeb.Persistence;
using MangaWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddScoped<IViewService, ViewService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IImageService, ImageService>();

// Đăng ký controllers
builder.Services.AddControllers();

var app = builder.Build();

// Cấu hình middleware
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();