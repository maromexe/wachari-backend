namespace Wachari.Api.Dtos;

public class MediaItemResponseDto
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public decimal Rating { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public List<string> Genres { get; set; } = [];
    public string PosterUrl { get; set; } = string.Empty;

    // Movie only
    public int? RuntimeMinutes { get; set; }

    // TV show only
    public int? NumberOfSeasons { get; set; }
    public int? NumberOfEpisodes { get; set; }
}