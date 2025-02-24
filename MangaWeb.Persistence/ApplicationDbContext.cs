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

        // Cấu hình quan hệ nhiều-nhiều giữa Manga và Tag
        modelBuilder.Entity<Manga>()
            .HasMany(m => m.Tags)
            .WithMany(t => t.Mangas)
            .UsingEntity(j => j.ToTable("MangaTags"));

        // Cấu hình quan hệ một-nhiều giữa Manga và Chapter
        modelBuilder.Entity<Chapter>()
            .HasOne(c => c.Manga)
            .WithMany(m => m.Chapters)
            .HasForeignKey(c => c.MangaId);

        // Cấu hình quan hệ một-nhiều giữa Chapter và ChapterImage
        modelBuilder.Entity<ChapterImage>()
            .HasOne(ci => ci.Chapter)
            .WithMany(c => c.ChapterImages)
            .HasForeignKey(ci => ci.ChapterId);

        // Cấu hình quan hệ một-nhiều giữa Manga và ReviewManga
        modelBuilder.Entity<ReviewManga>()
            .HasOne(r => r.Manga)
            .WithMany(m => m.ReviewMangas)
            .HasForeignKey(r => r.MangaId);

        // Cấu hình quan hệ một-nhiều giữa Manga và Author
        modelBuilder.Entity<Manga>()
            .HasOne(m => m.Author)
            .WithMany(a => a.Mangas)
            .HasForeignKey(m => m.AuthorId);

        // Cấu hình quan hệ nhiều-nhiều giữa Role và Permission
        modelBuilder.Entity<RolePermission>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });

        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId);

        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(rp => rp.PermissionId);
    }

    // Các DbSet cho các entity
    public DbSet<Manga> Mangas { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<ChapterImage> ChapterImages { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<ReviewManga> ReviewMangas { get; set; }
    public DbSet<ChapterView> ChapterViews { get; set; }
    public DbSet<MangaView> MangaViews { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
}