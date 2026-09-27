using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wachari.Api.Data;
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

    // GET /api/media
    // GET /api/media?type=movie
    // GET /api/media?type=tvShow
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
}

