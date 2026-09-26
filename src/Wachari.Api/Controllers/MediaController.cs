using Microsoft.AspNetCore.Mvc;
using Wachari.Api.Models;

namespace Wachari.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaController : ControllerBase
{
    // temp
    private static readonly List<MediaItem> Items =
    [
        new Movie
        {
            Id = 1,
            Title = "Pulp Fiction",
            Tagline = "Just because you are a character doesn't mean you have character.",
            Overview = "The lives of two mob hitmen, a boxer and a pair of diner bandits intertwine in four tales of violence and redemption.",
            Rating = 8.5,
            ReleaseDate = new DateOnly(1994, 10, 14),
            Genres = ["Thriller", "Crime"],
            PosterUrl = "/images/posters/pulp-fiction.jpg",
            RuntimeMinutes = 154
        },
        new Movie
        {
            Id = 2,
            Title = "The Hateful Eight",
            Tagline = "No one comes up here without a damn good reason.",
            Overview = "Bounty hunters seek shelter from a raging blizzard and get caught up in a plot of betrayal and deception.",
            Rating = 7.7,
            ReleaseDate = new DateOnly(2015, 12, 25),
            Genres = ["Drama", "Mystery", "Western"],
            PosterUrl = "/images/posters/hateful-eight.jpg",
            RuntimeMinutes = 168
        },
        new TvShow
        {
            Id = 3,
            Title = "Rick and Morty",
            Tagline = "Science. Adventure. Burps.",
            Overview = "A brilliant but reckless scientist drags his grandson on dangerous adventures across the multiverse.",
            Rating = 8.7,
            ReleaseDate = new DateOnly(2013, 12, 2),
            Genres = ["Animation", "Comedy", "Sci-Fi & Fantasy"],
            PosterUrl = "/images/posters/rick-and-morty.jpg",
            NumberOfSeasons = 8,
            NumberOfEpisodes = 81
        },
        new TvShow
        {
            Id = 4,
            Title = "The Office",
            Tagline = "A mockumentary about office life.",
            Overview = "The everyday lives of office employees at the Scranton branch of the Dunder Mifflin Paper Company.",
            Rating = 8.6,
            ReleaseDate = new DateOnly(2005, 3, 24),
            Genres = ["Comedy"],
            PosterUrl = "/images/posters/the-office.jpg",
            NumberOfSeasons = 9,
            NumberOfEpisodes = 201
        }
    ];

    // GET /api/media
    // GET /api/media?type=movie
    // GET /api/media?type=tvShow
    [HttpGet]
    public ActionResult<List<MediaItem>> GetAll([FromQuery] MediaType? type)
    {
        IEnumerable<MediaItem> result = type switch
        {
            MediaType.Movie => Items.OfType<Movie>(),
            MediaType.TvShow => Items.OfType<TvShow>(),
            _ => Items
        };

        return result.ToList();
    }

    // GET /api/media/1
    [HttpGet("{id:int}")]
    public ActionResult<MediaItem> GetById(int id)
    {
        var item = Items.FirstOrDefault(i => i.Id == id);

        if (item is null)
        {
            return NotFound();
        }

        return item;
    }
}
