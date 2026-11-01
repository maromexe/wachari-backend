using Microsoft.EntityFrameworkCore;
using Wachari.Api.Models;

namespace Wachari.Api.Data;

public class WachariDbContext : DbContext
{
    public WachariDbContext(DbContextOptions<WachariDbContext> options)
        : base(options)
    {
    }

    public DbSet<MediaItem> MediaItems => Set<MediaItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var mediaItem = modelBuilder.Entity<MediaItem>();

        // TPH: movies and TV shows share one table, the "Type" column tells them apart
        mediaItem
            .HasDiscriminator<string>("Type")
            .HasValue<Movie>("movie")
            .HasValue<TvShow>("tvShow");

        mediaItem.Property<string>("Type").HasMaxLength(20);

        mediaItem.Property(m => m.Title).HasMaxLength(200);
        mediaItem.Property(m => m.Tagline).HasMaxLength(300);
        mediaItem.Property(m => m.Overview).HasMaxLength(2000);
        mediaItem.Property(m => m.PosterUrl).HasMaxLength(500);
        mediaItem.Property(m => m.Rating).HasPrecision(3, 1);
    }
}
