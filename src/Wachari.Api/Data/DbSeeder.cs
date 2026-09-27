using Wachari.Api.Models;

namespace Wachari.Api.Data;

public static class DbSeeder
{
    public static void Seed(WachariDbContext db)
    {
        if (db.MediaItems.Any())
        {
            return;
        }

        db.MediaItems.AddRange(
            new Movie
            {
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
                Title = "The Office",
                Tagline = "A mockumentary about office life.",
                Overview = "The everyday lives of office employees at the Scranton branch of the Dunder Mifflin Paper Company.",
                Rating = 8.6,
                ReleaseDate = new DateOnly(2005, 3, 24),
                Genres = ["Comedy"],
                PosterUrl = "/images/posters/the-office.jpg",
                NumberOfSeasons = 9,
                NumberOfEpisodes = 201
            });

        db.SaveChanges();
    }
}
