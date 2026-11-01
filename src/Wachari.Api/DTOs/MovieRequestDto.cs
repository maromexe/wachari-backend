using System.ComponentModel.DataAnnotations;

namespace Wachari.Api.Dtos;

public class MovieRequestDto : MediaItemRequestDto
{
    [Required]
    [Range(1, 1000)]
    public int? RuntimeMinutes { get; set; }
}
