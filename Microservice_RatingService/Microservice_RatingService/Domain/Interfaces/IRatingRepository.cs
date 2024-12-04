using Microservice_RatingService.Application.Dtos;

namespace Microservice_RatingService.Domain.Interfaces
{
    public interface IRatingRepository
    {
        Task<RatingReadDto> CreateAsync(RatingCreateDto ratingCreateDto);
        Task<IEnumerable<RatingReadDto>> GetAllAsync();
        Task<RatingReadDto> GetByIdAsync(Guid id);
        Task<RatingReadDto> UpdateAsync(Guid id, RatingUpdateDto ratingUpdateDto);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<RatingReadDto>> GetRatingsBySellerIdAsync(Guid sellerId);
        Task<IEnumerable<RatingReadDto>> GetRatingsByBuyerIdAsync(Guid buyerId);
        Task<SellerRatingStatsDto> GetSellerRatingStatsAsync(Guid sellerId);
    }
}
