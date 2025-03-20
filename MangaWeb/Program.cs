using MangaWeb.Infrastructure; 
using MangaWeb.Persistence; 
using MangaWeb.Application; 
using MangaWeb.Api; 
using MangaWeb.Domain.Abstractions.ApplicationServices; 
using Microsoft.EntityFrameworkCore;
using MangaWeb.Application.Services;
using MangaWeb.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
//Đăng ký AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add this service to use IHttpContextAccessor in another class
builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCORS();

#region Persistence 
builder.Services.AddSqlServerPersistence(builder.Configuration);
builder.Services.AddRepositoryUnitOfWork();
#endregion

#region Infrastructure
builder.Services.AddServicesInfrastructure();
builder.Services.Configure<JwtOption>(builder.Configuration.GetSection("JwtOption"));
#endregion

#region Application
builder.Services.AddServicesApplication(); 
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
InitDatabase(app);
app.Use(async (context, next) =>
{
    if (context.Items.ContainsKey("Token") && context.Items["Token"] is string token)
    {
        Console.WriteLine($" Gắn Bearer Token vào request: {token}");
        context.Request.Headers["Authorization"] = $"Bearer {token}";
    }
    else
    {
        Console.WriteLine(" Không tìm thấy Token trong HttpContext.Items");
    }

    await next();
});


app.Run();

void InitDatabase(IApplicationBuilder app)
{
    var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
    if (scopeFactory == null) throw new Exception("IServiceScopeFactory not found!");

    using var serviceScope = scopeFactory.CreateScope();
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();

    var userService = serviceScope.ServiceProvider.GetService<IUserService>();
    userService?.InitializeUserAdminAsync().Wait();
}

