using MangaWeb.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MangaWeb.Persistence;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<GeneralImage>().ToTable("GeneralImages");
        modelBuilder.Entity<Chapter>()
            .Property(c => c.ImagePaths)
            .HasConversion(
                v => string.Join(";", v), // Chuyển List<string> thành chuỗi JSON
                v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList() // Chuyển chuỗi JSON thành List<string>
            );

                base.OnModelCreating(modelBuilder);
    }

    // Các DbSet cho các entity
    public DbSet<Manga> Mangas { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<ReviewManga> ReviewMangas { get; set; }
    public DbSet<ChapterView> ChapterViews { get; set; }
    public DbSet<MangaView> MangaViews { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    // Thêm GeneralImages vào DbSet
    public DbSet<GeneralImage> GeneralImages { get; set; }
}
