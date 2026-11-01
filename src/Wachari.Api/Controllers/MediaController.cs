using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wachari.Api.Data;
using Wachari.Api.Dtos;
using Wachari.Api.Models;

namespace Wachari.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaController : ControllerBase
{
    private readonly WachariDbContext _db;

    public MediaController(WachariDbContext db)
    {
        _db = db;
    }

    // GET /api/media?type=movie
    [HttpGet]
    public async Task<ActionResult<List<MediaItem>>> GetAll([FromQuery] MediaType? type)
    {
        IQueryable<MediaItem> query = type switch
        {
            MediaType.Movie => _db.MediaItems.OfType<Movie>(),
            MediaType.TvShow => _db.MediaItems.OfType<TvShow>(),
            _ => _db.MediaItems
        };

        return await query.AsNoTracking().ToListAsync();
    }

    // GET /api/media/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MediaItem>> GetById(int id)
    {
        var item = await _db.MediaItems
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);

        if (item is null)
        {
            return NotFound();
        }

        return item;
    }

    // POST /api/media/movies
    [HttpPost("movies")]
    public async Task<ActionResult<MediaItem>> CreateMovie(MovieRequestDto dto)
    {
        var movie = new Movie();
        ApplyMovieFields(dto, movie);

        _db.MediaItems.Add(movie);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
    }

    // POST /api/media/tv-shows
    [HttpPost("tv-shows")]
    public async Task<ActionResult<MediaItem>> CreateTvShow(TvShowRequestDto dto)
    {
        var tvShow = new TvShow();
        ApplyTvShowFields(dto, tvShow);

        _db.MediaItems.Add(tvShow);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = tvShow.Id }, tvShow);
    }

    // PUT /api/media/movies/1
    [HttpPut("movies/{id:int}")]
    public async Task<IActionResult> UpdateMovie(int id, MovieRequestDto dto)
    {
        var movie = await _db.MediaItems
            .OfType<Movie>()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie is null)
        {
            return NotFound();
        }

        ApplyMovieFields(dto, movie);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    // PUT /api/media/tv-shows/3
    [HttpPut("tv-shows/{id:int}")]
    public async Task<IActionResult> UpdateTvShow(int id, TvShowRequestDto dto)
    {
        var tvShow = await _db.MediaItems
            .OfType<TvShow>()
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tvShow is null)
        {
            return NotFound();
        }

        ApplyTvShowFields(dto, tvShow);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    // DELETE /api/media/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.MediaItems.FindAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        _db.MediaItems.Remove(item);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    // Mapping: copy data from DTOs to entities
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
