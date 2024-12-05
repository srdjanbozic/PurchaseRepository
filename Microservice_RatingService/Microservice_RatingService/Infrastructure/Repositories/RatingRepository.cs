using AutoMapper;
using Microservice_RatingService.Application.Dtos;
using Microservice_RatingService.Application.Exceptions;
using Microservice_RatingService.Domain.Entities;
using Microservice_RatingService.Domain.Interfaces;
using Microservice_RatingService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Microservice_RatingService.Infrastructure.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<RatingRepository> _logger;

        public RatingRepository(AppDbContext context, IMapper mapper, ILogger<RatingRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RatingReadDto> CreateAsync(RatingCreateDto ratingCreateDto)
        {
            var rating = _mapper.Map<Rating>(ratingCreateDto);

            // Verify if buyer exists
            if (!await _context.Ratings.AnyAsync(r => r.Buyer.BuyerId == ratingCreateDto.BuyerId))
            {
                throw new BuyerNotFoundException(ratingCreateDto.BuyerId);
            }

            // Verify if seller exists
            if (!await _context.Ratings.AnyAsync(r => r.Seller.SellerId == ratingCreateDto.SellerId))
            {
                throw new SellerNotFoundException(ratingCreateDto.SellerId);
            }

            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Created rating with ID: {RatingId}", rating.RatingId);
            return _mapper.Map<RatingReadDto>(rating);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                throw new RatingNotFoundException(id);
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted rating with ID: {RatingId}", id);
            return true;
        }

        public async Task<IEnumerable<RatingReadDto>> GetAllAsync()
        {
            var ratings = await _context.Ratings.ToListAsync();
            _logger.LogInformation("Retrieved {Count} ratings from database", ratings.Count);
            return _mapper.Map<IEnumerable<RatingReadDto>>(ratings);
        }

        public async Task<RatingReadDto> GetByIdAsync(Guid id)
        {
            var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.RatingId == id);
            if (rating == null)
            {
                _logger.LogWarning("Rating with ID {RatingId} not found", id);
                throw new RatingNotFoundException(id);
            }

            _logger.LogInformation("Retrieved rating with ID: {RatingId}", id);
            return _mapper.Map<RatingReadDto>(rating);
        }

        public async Task<RatingReadDto> UpdateAsync(Guid id, RatingUpdateDto ratingUpdateDto)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                throw new RatingNotFoundException(id);
            }

            _mapper.Map(ratingUpdateDto, rating);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated rating with ID: {RatingId}", id);
            return _mapper.Map<RatingReadDto>(rating);
        }

        public async Task<IEnumerable<RatingReadDto>> GetRatingsBySellerIdAsync(Guid sellerId)
        {
            var ratings = await _context.Ratings
                .Where(r => r.Seller.SellerId == sellerId)
                .ToListAsync();

            if (!ratings.Any())
            {
                throw new SellerNotFoundException(sellerId);
            }

            _logger.LogInformation("Retrieved {Count} ratings for seller {SellerId}", ratings.Count, sellerId);
            return _mapper.Map<IEnumerable<RatingReadDto>>(ratings);
        }

        public async Task<IEnumerable<RatingReadDto>> GetRatingsByBuyerIdAsync(Guid buyerId)
        {
            var ratings = await _context.Ratings
                .Where(r => r.Buyer.BuyerId == buyerId)
                .ToListAsync();

            if (!ratings.Any())
            {
                throw new BuyerNotFoundException(buyerId);
            }

            _logger.LogInformation("Retrieved {Count} ratings from buyer {BuyerId}", ratings.Count, buyerId);
            return _mapper.Map<IEnumerable<RatingReadDto>>(ratings);
        }

        public async Task<SellerRatingStatsDto> GetSellerRatingStatsAsync(Guid sellerId)
        {
            try
            {
                // First check if we have any ratings for this seller
                var ratings = await _context.Ratings
                    .Include(r => r.Seller) // Include seller data
                    .Where(r => r.Seller.SellerId == sellerId)
                    .ToListAsync();

                if (ratings.Count == 0)
                {
                    _logger.LogWarning("Seller with ID {SellerId} not found", sellerId);
                    throw new SellerNotFoundException(sellerId);
                }

                var seller = ratings[0].Seller;
                if (seller == null)
                {
                    _logger.LogWarning("Seller with ID {SellerId} not found", sellerId);
                    throw new SellerNotFoundException(sellerId);
                }

                var stats = new SellerRatingStatsDto
                {
                    SellerId = sellerId,
                    SellerUsername = seller.SellerUsername,
                    AverageRating = ratings.Average(r => r.Grade),
                    TotalRatings = ratings.Count,
                    RatingDistribution = ratings
                        .GroupBy(r => r.Grade)
                        .ToDictionary(g => g.Key, g => g.Count())
                };

                _logger.LogInformation("Retrieved rating stats for seller {SellerId}", sellerId);
                return stats;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving stats for seller {SellerId}", sellerId);
                throw new ApplicationException($"An error occurred while retrieving stats for seller with ID {sellerId}.", ex);
            }
        }
    }

}

