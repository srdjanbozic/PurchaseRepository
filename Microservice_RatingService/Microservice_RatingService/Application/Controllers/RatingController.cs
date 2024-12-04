using FluentValidation;
using Microservice_RatingService.Application.Dtos;
using Microservice_RatingService.Application.Models;
using Microservice_RatingService.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Microservice_RatingService.Application.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly ILogger<RatingController> _logger;
        private readonly IValidator<RatingCreateDto> _createValidator;
        private readonly IValidator<RatingUpdateDto> _updateValidator;

        public RatingController(IRatingRepository ratingRepository, ILogger<RatingController> logger, IValidator<RatingCreateDto> createValidator,
        IValidator<RatingUpdateDto> updateValidator)
        {
            _ratingRepository = ratingRepository;
            _logger = logger;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        /// <summary>
        /// Add new rating to the database
        /// </summary>
        /// <param name="ratingCreateDto"></param>
        /// <returns>Return status code 200 if rating is successfully added!</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Adds new rating to the database")]
        [SwaggerResponse(200, "Successfully added rating to the database!")]
        [SwaggerResponse(400, "Bad request!")]
        [SwaggerResponse(500, "Something is wrong with the server!")]
        //POST: api/values
        public async Task<IActionResult> CreateRating(RatingCreateDto ratingCreateDto)
        {
            _logger.LogInformation("Creating new rating {@RatingCreateDto}", ratingCreateDto);
            // pre nego sto kreiramo novi rating moramo uraditi validaciju propertija, guid ne sme biti prazan itd
            var validationResult = await _createValidator.ValidateAsync(ratingCreateDto);
            if (!validationResult.IsValid)
            {
                var errorResponse = new ApiErrorResponse("Validation failed");
                errorResponse.Errors = validationResult.Errors
                    .Select(error => new ValidationError
                    {
                        Field = error.PropertyName,
                        Message = error.ErrorMessage
                    })
                    .ToList();

                return BadRequest(errorResponse);
            }
            try
            {
                var rating = await _ratingRepository.CreateAsync(ratingCreateDto);
                _logger.LogInformation("Rating created successfully with ID: {RatingId}", rating.RatingId);
                return CreatedAtAction(nameof(GetRatingById), new { id = rating.RatingId }, rating);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating rating");
                return StatusCode(500, "Internal server error while creating rating");
            }
        }
        /// <summary>
        /// Return all ratings from database
        /// </summary>
        /// <returns>Return all ratings from database - Status code 200 </returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Return all ratings from the database")]
        [SwaggerResponse(200, "Successfully returns all ratings!")]
        [SwaggerResponse(400, "Bad request!")]
        [SwaggerResponse(500, "Something is wrong with the server!")]
        // GET:api/ratings
        public async Task<IActionResult> GetRatings()
        {
            _logger.LogInformation("Retrieving all ratings");
            try
            {
                var ratings = await _ratingRepository.GetAllAsync();
                _logger.LogInformation("Retrieved {Count} ratings", ratings.Count());
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all ratings");
                return StatusCode(500, "Internal server error while retrieving ratings");
            }

        }
        /// <summary>
        /// Return rating with specified id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return rating with specified id from database - Status code 200</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Return rating with specified Id from the database")]
        [SwaggerResponse(200, "Successfully return specified rating!")]
        [SwaggerResponse(400, "Bad request!")]
        [SwaggerResponse(500, "Something is wrong with the server!")]
        //GET: api/ratings/1
        public async Task<IActionResult> GetRatingById(Guid id)
        {
            _logger.LogInformation("Retrieving rating with ID: {RatingId}", id);
            try
            {
                var rating = await _ratingRepository.GetByIdAsync(id);
                if (rating == null)
                {
                    _logger.LogWarning("Rating with ID: {RatingId} not found", id);
                    return NotFound();
                }
                return Ok(rating);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving rating with ID: {RatingId}", id);
                return StatusCode(500, "Internal server error while retrieving rating");
            }
        }
        /// <summary>
        /// Update rating with specified id in database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ratingUpdateDto"></param>
        /// <returns>Updated rating - Status code 200</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Update rating with specifed Id in the database")]
        [SwaggerResponse(200, "Successfully updated rating!")]
        [SwaggerResponse(400, "Bad request!")]
        [SwaggerResponse(500, "Something is wrong with the server!")]
        //PUT: api/values/1
        public async Task<IActionResult> UpdateRating(Guid id, RatingUpdateDto ratingUpdateDto)
        {
            _logger.LogInformation("Updating rating {RatingId} with {@RatingUpdateDto}", id, ratingUpdateDto);
            // validacija pre nego sto izvrimo update rating da utvrdimo da sve zadovoljava propisana pravila pre update
            var validationResult = await _updateValidator.ValidateAsync(ratingUpdateDto);
            if (!validationResult.IsValid)
            {
                var errorResponse = new ApiErrorResponse("Validation failed");
                errorResponse.Errors = validationResult.Errors
                    .Select(error => new ValidationError
                    {
                        Field = error.PropertyName,
                        Message = error.ErrorMessage
                    })
                    .ToList();

                return BadRequest(errorResponse);
            }
            try
            {
                var updatedRating = await _ratingRepository.UpdateAsync(id, ratingUpdateDto);
                if (updatedRating == null)
                {
                    _logger.LogWarning("Rating with ID: {RatingId} not found for update", id);
                    return NotFound();
                }
                _logger.LogInformation("Rating {RatingId} updated successfully", id);
                return Ok(updatedRating);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating rating {RatingId}", id);
                return StatusCode(500, "Internal server error while updating rating");
            }
        }
        /// <summary>
        /// Delete rating by id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return status code 200 if rating is successfully deleted from database!</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Delete rating by Id from the database")]
        [SwaggerResponse(200, "Successfully deleted rating!")]
        [SwaggerResponse(204, "Succesfully deleted rating, no additional content!")]
        [SwaggerResponse(400, "Bad request!")]
        [SwaggerResponse(500, "Something is wrong with the server!")]
        //DELETE: api/ratings/1
        public async Task<IActionResult> DeleteRating(Guid id)
        {
            _logger.LogInformation("Deleting rating with ID: {RatingId}", id);
            try
            {
                var success = await _ratingRepository.DeleteAsync(id);
                if (!success)
                {
                    _logger.LogWarning("Rating with ID: {RatingId} not found for deletion", id);
                    return NotFound();
                }
                _logger.LogInformation("Rating {RatingId} deleted successfully", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting rating {RatingId}", id);
                return StatusCode(500, "Internal server error while deleting rating");
            }
        }
        [HttpGet("seller/{sellerId}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all ratings for a specific seller")]
        [SwaggerResponse(200, "Successfully retrieved seller ratings")]
        [SwaggerResponse(404, "Seller not found")]
        public async Task<IActionResult> GetRatingsBySeller(Guid sellerId)
        {
            try
            {
                var ratings = await _ratingRepository.GetRatingsBySellerIdAsync(sellerId);
                if (!ratings.Any())
                    return NotFound($"No ratings found for seller with ID: {sellerId}");

                return Ok(ratings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving ratings for seller {SellerId}", sellerId);
                return StatusCode(500, "Internal server error while retrieving seller ratings");
            }
        }

        [HttpGet("buyer/{buyerId}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all ratings from a specific buyer")]
        [SwaggerResponse(200, "Successfully retrieved buyer ratings")]
        [SwaggerResponse(404, "Buyer not found")]
        public async Task<IActionResult> GetRatingsByBuyer(Guid buyerId)
        {
            try
            {
                var ratings = await _ratingRepository.GetRatingsByBuyerIdAsync(buyerId);
                if (!ratings.Any())
                    return NotFound($"No ratings found for buyer with ID: {buyerId}");

                return Ok(ratings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving ratings for buyer {BuyerId}", buyerId);
                return StatusCode(500, "Internal server error while retrieving buyer ratings");
            }
        }

        [HttpGet("seller/{sellerId}/stats")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get rating statistics for a specific seller")]
        [SwaggerResponse(200, "Successfully retrieved seller statistics")]
        [SwaggerResponse(404, "Seller not found")]
        public async Task<IActionResult> GetSellerRatingStats(Guid sellerId)
        {
            try
            {
                var stats = await _ratingRepository.GetSellerRatingStatsAsync(sellerId);
                if (stats == null)
                    return NotFound($"No statistics found for seller with ID: {sellerId}");

                return Ok(stats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving stats for seller {SellerId}", sellerId);
                return StatusCode(500, "Internal server error while retrieving seller statistics");
            }
        }
    }

}


