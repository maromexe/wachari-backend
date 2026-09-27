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
        modelBuilder.Entity<MediaItem>()
            .HasDiscriminator<string>("Type")
            .HasValue<Movie>("movie")
            .HasValue<TvShow>("tvShow");
    }
}
