using Microsoft.AspNetCore.Mvc;
using Wachari.Api.Dtos;
using Wachari.Api.Models;
using Wachari.Api.Services;

namespace Wachari.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaController : ControllerBase
{
    private readonly IMediaService _mediaService;

    public MediaController(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    // GET /api/media
    // GET /api/media?type=movie|tvShow
    [HttpGet]
    public async Task<ActionResult<List<MediaItemResponseDto>>> GetAll(
        [FromQuery] MediaType? type, CancellationToken ct)
    {
        return await _mediaService.GetAllAsync(type, ct);
    }

    // GET /api/media/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MediaItemResponseDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _mediaService.GetByIdAsync(id, ct);

        if (item is null)
        {
            return NotFound();
        }

        return item;
    }

    // POST /api/media/movies
    [HttpPost("movies")]
    public async Task<ActionResult<MediaItemResponseDto>> CreateMovie(
        MovieRequestDto dto, CancellationToken ct)
    {
        var created = await _mediaService.CreateMovieAsync(dto, ct);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // POST /api/media/tv-shows
    [HttpPost("tv-shows")]
    public async Task<ActionResult<MediaItemResponseDto>> CreateTvShow(
        TvShowRequestDto dto, CancellationToken ct)
    {
        var created = await _mediaService.CreateTvShowAsync(dto, ct);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT /api/media/movies/1
    [HttpPut("movies/{id:int}")]
    public async Task<IActionResult> UpdateMovie(int id, MovieRequestDto dto, CancellationToken ct)
    {
        var updated = await _mediaService.UpdateMovieAsync(id, dto, ct);

        return updated ? NoContent() : NotFound();
    }

    // PUT /api/media/tv-shows/3
    [HttpPut("tv-shows/{id:int}")]
    public async Task<IActionResult> UpdateTvShow(int id, TvShowRequestDto dto, CancellationToken ct)
    {
        var updated = await _mediaService.UpdateTvShowAsync(id, dto, ct);

        return updated ? NoContent() : NotFound();
    }

    // DELETE /api/media/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await _mediaService.DeleteAsync(id, ct);

        return deleted ? NoContent() : NotFound();
    }
}
