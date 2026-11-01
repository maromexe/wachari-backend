using Wachari.Api.Dtos;
using Wachari.Api.Models;

namespace Wachari.Api.Services;

public interface IMediaService
{
    Task<List<MediaItemResponseDto>> GetAllAsync(MediaType? type, CancellationToken ct);

    Task<MediaItemResponseDto?> GetByIdAsync(int id, CancellationToken ct);

    Task<MediaItemResponseDto> CreateMovieAsync(MovieRequestDto dto, CancellationToken ct);

    Task<MediaItemResponseDto> CreateTvShowAsync(TvShowRequestDto dto, CancellationToken ct);

    Task<bool> UpdateMovieAsync(int id, MovieRequestDto dto, CancellationToken ct);

    Task<bool> UpdateTvShowAsync(int id, TvShowRequestDto dto, CancellationToken ct);

    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
