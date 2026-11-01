using System.ComponentModel.DataAnnotations;

namespace Wachari.Api.Dtos;

public abstract class MediaItemRequestDto
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(300)]
    public string Tagline { get; set; } = string.Empty;

    [StringLength(2000)]
    public string Overview { get; set; } = string.Empty;

    [Required]
    [Range(typeof(decimal), "0", "10")]
    public decimal? Rating { get; set; }

    [Required]
    public DateOnly? ReleaseDate { get; set; }

    public List<string> Genres { get; set; } = [];

    [StringLength(500)]
    public string PosterUrl { get; set; } = string.Empty;
}
