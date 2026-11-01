using System.ComponentModel.DataAnnotations;

namespace Wachari.Api.Dtos;

public class TvShowRequestDto : MediaItemRequestDto
{
    [Required]
    [Range(1, 100)]
    public int? NumberOfSeasons { get; set; }

    [Required]
    [Range(1, 10000)]
    public int? NumberOfEpisodes { get; set; }
}
