using Microsoft.EntityFrameworkCore;
using SeventyModel;
namespace SeventyData.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BandModel> Bands { get; set; }
        public DbSet<AlbumModel> Album { get; set; }
        public DbSet<SongModel> Song { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    //Relacionamento: Banda 1:N Álbum
    modelBuilder.Entity<AlbumModel>()
        .HasOne(a => a.Band)
        .WithMany(b => b.Albums)
        .HasForeignKey(a => a.BandId)
        .OnDelete(DeleteBehavior.Cascade);

    //Relacionamento: Álbum 1:N Música
    modelBuilder.Entity<SongModel>()
        .HasOne(s => s.Album)
        .WithMany(a => a.Songs)
        .HasForeignKey(s => s.AlbumId)
        .OnDelete(DeleteBehavior.Cascade);

    //Relacionamento: Banda 1:N Música (acesso direto da banda à música)
    modelBuilder.Entity<SongModel>()
        .HasOne(s => s.Band)
        .WithMany() 
        .HasForeignKey(s => s.BandId)
        .OnDelete(DeleteBehavior.Restrict); 
}
    }
}
