using Microsoft.EntityFrameworkCore;
using Wachari.Api.Data;
using Wachari.Api.Dtos;
using Wachari.Api.Models;

namespace Wachari.Api.Services;

public class MediaService : IMediaService
{
    private readonly WachariDbContext _db;

    public MediaService(WachariDbContext db)
    {
        _db = db;
    }

    public async Task<List<MediaItemResponseDto>> GetAllAsync(MediaType? type, CancellationToken ct)
    {
        IQueryable<MediaItem> query = type switch
        {
            MediaType.Movie => _db.MediaItems.OfType<Movie>(),
            MediaType.TvShow => _db.MediaItems.OfType<TvShow>(),
            _ => _db.MediaItems
        };

        var items = await query
            .AsNoTracking()
            .OrderBy(i => i.Id)
            .ToListAsync(ct);

        return items.Select(ToResponse).ToList();
    }

    public async Task<MediaItemResponseDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        var item = await _db.MediaItems
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id, ct);

        return item is null ? null : ToResponse(item);
    }

    public async Task<MediaItemResponseDto> CreateMovieAsync(MovieRequestDto dto, CancellationToken ct)
    {
        var movie = new Movie();
        ApplyMovieFields(dto, movie);

        _db.MediaItems.Add(movie);
        await _db.SaveChangesAsync(ct);

        return ToResponse(movie);
    }

    public async Task<MediaItemResponseDto> CreateTvShowAsync(TvShowRequestDto dto, CancellationToken ct)
    {
        var tvShow = new TvShow();
        ApplyTvShowFields(dto, tvShow);

        _db.MediaItems.Add(tvShow);
        await _db.SaveChangesAsync(ct);

        return ToResponse(tvShow);
    }

    public async Task<bool> UpdateMovieAsync(int id, MovieRequestDto dto, CancellationToken ct)
    {
        var movie = await _db.MediaItems
            .OfType<Movie>()
            .FirstOrDefaultAsync(m => m.Id == id, ct);

        if (movie is null)
        {
            return false;
        }

        ApplyMovieFields(dto, movie);
        await _db.SaveChangesAsync(ct);

        return true;
    }

    public async Task<bool> UpdateTvShowAsync(int id, TvShowRequestDto dto, CancellationToken ct)
    {
        var tvShow = await _db.MediaItems
            .OfType<TvShow>()
            .FirstOrDefaultAsync(t => t.Id == id, ct);

        if (tvShow is null)
        {
            return false;
        }

        ApplyTvShowFields(dto, tvShow);
        await _db.SaveChangesAsync(ct);

        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var item = await _db.MediaItems.FindAsync([id], ct);

        if (item is null)
        {
            return false;
        }

        _db.MediaItems.Remove(item);
        await _db.SaveChangesAsync(ct);

        return true;
    }

    // Mapping: entity -> response DTO
    private static MediaItemResponseDto ToResponse(MediaItem item)
    {
        var dto = new MediaItemResponseDto
        {
            Id = item.Id,
            Title = item.Title,
            Tagline = item.Tagline,
            Overview = item.Overview,
            Rating = item.Rating,
            ReleaseDate = item.ReleaseDate,
            Genres = item.Genres,
            PosterUrl = item.PosterUrl
        };

        switch (item)
        {
            case Movie movie:
                dto.Type = "movie";
                dto.RuntimeMinutes = movie.RuntimeMinutes;
                break;
            case TvShow tvShow:
                dto.Type = "tvShow";
                dto.NumberOfSeasons = tvShow.NumberOfSeasons;
                dto.NumberOfEpisodes = tvShow.NumberOfEpisodes;
                break;
        }

        return dto;
    }

    // Mapping: request DTO -> entity
    private static void ApplyMovieFields(MovieRequestDto dto, Movie movie)
    {
        ApplyCommonFields(dto, movie);
        movie.RuntimeMinutes = dto.RuntimeMinutes!.Value;
    }

    private static void ApplyTvShowFields(TvShowRequestDto dto, TvShow tvShow)
    {
        ApplyCommonFields(dto, tvShow);
        tvShow.NumberOfSeasons = dto.NumberOfSeasons!.Value;
        tvShow.NumberOfEpisodes = dto.NumberOfEpisodes!.Value;
    }

    private static void ApplyCommonFields(MediaItemRequestDto dto, MediaItem item)
    {
        item.Title = dto.Title;
        item.Tagline = dto.Tagline;
        item.Overview = dto.Overview;
        item.Rating = dto.Rating!.Value;
        item.ReleaseDate = dto.ReleaseDate!.Value;
        item.Genres = dto.Genres;
        item.PosterUrl = dto.PosterUrl;
    }
}
