using System.Text.Json.Serialization;

namespace Wachari.Api.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(Movie), "movie")]
[JsonDerivedType(typeof(TvShow), "tvShow")]
public abstract class MediaItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public double Rating { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public List<string> Genres { get; set; } = [];
    public string PosterUrl { get; set; } = string.Empty;
}
