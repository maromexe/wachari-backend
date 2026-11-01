namespace Wachari.Api.Models;

public abstract class MediaItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public decimal Rating { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public List<string> Genres { get; set; } = [];
    public string PosterUrl { get; set; } = string.Empty;
}
