using FluentValidation;
using Purchase_Microservice.Application.Dtos;
using Purchase_Microservice.Application.Models;
using Purchase_Microservice.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace Purchase_Microservice.Application.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ILogger<PurchaseController> _logger;
        private readonly IValidator<PurchaseCreateDto> _createValidator;
        private readonly IValidator<PurchaseUpdateDto> _updateValidator;

        public PurchaseController(
            IPurchaseRepository purchaseRepository,
            ILogger<PurchaseController> logger,
            IValidator<PurchaseCreateDto> createValidator,
            IValidator<PurchaseUpdateDto> updateValidator)
        {
            _purchaseRepository = purchaseRepository;
            _logger = logger;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Adds new purchase to the database")]
        [SwaggerResponse(200, "Successfully added purchase to the database!")]
        [SwaggerResponse(400, "Bad request!")]
        [SwaggerResponse(500, "Something is wrong with the server!")]
        public async Task<IActionResult> CreatePurchase(PurchaseCreateDto purchaseCreateDto)
        {
            _logger.LogInformation("Creating new purchase {@PurchaseCreateDto}", purchaseCreateDto);

            // Validate request data
            var validationResult = await _createValidator.ValidateAsync(purchaseCreateDto);
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
                var purchase = await _purchaseRepository.CreateAsync(purchaseCreateDto);
                _logger.LogInformation("Purchase created successfully with ID: {PurchaseId}", purchase.PurchaseId);
                return CreatedAtAction(nameof(GetPurchaseById), new { id = purchase.PurchaseId }, purchase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating purchase");
                return StatusCode(500, "Internal server error while creating purchase");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Return all purchases from the database")]
        [SwaggerResponse(200, "Successfully returns all purchases!")]
        [SwaggerResponse(400, "Bad request!")]
        [SwaggerResponse(500, "Something is wrong with the server!")]
        public async Task<IActionResult> GetPurchases()
        {
            _logger.LogInformation("Retrieving all purchases");
            try
            {
                var purchases = await _purchaseRepository.GetAllAsync();
                _logger.LogInformation("Retrieved {Count} purchases", purchases.Count());
                return Ok(purchases);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all purchases");
                return StatusCode(500, "Internal server error while retrieving purchases");
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Return purchase with specified Id from the database")]
        [SwaggerResponse(200, "Successfully return specified purchase!")]
        [SwaggerResponse(400, "Bad request!")]
        [SwaggerResponse(500, "Something is wrong with the server!")]
        public async Task<IActionResult> GetPurchaseById(Guid id)
        {
            _logger.LogInformation("Retrieving purchase with ID: {PurchaseId}", id);
            try
            {
                var purchase = await _purchaseRepository.GetByIdAsync(id);
                if (purchase == null)
                {
                    _logger.LogWarning("Purchase with ID: {PurchaseId} not found", id);
                    return NotFound();
                }
                return Ok(purchase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving purchase with ID: {PurchaseId}", id);
                return StatusCode(500, "Internal server error while retrieving purchase");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Update purchase with specified Id in the database")]
        [SwaggerResponse(200, "Successfully updated purchase!")]
        [SwaggerResponse(400, "Bad request!")]
        [SwaggerResponse(500, "Something is wrong with the server!")]
        public async Task<IActionResult> UpdatePurchase(Guid id, PurchaseUpdateDto purchaseUpdateDto)
        {
            _logger.LogInformation("Updating purchase {PurchaseId} with {@PurchaseUpdateDto}", id, purchaseUpdateDto);

            var validationResult = await _updateValidator.ValidateAsync(purchaseUpdateDto);
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
                var updatedPurchase = await _purchaseRepository.UpdateAsync(id, purchaseUpdateDto);
                if (updatedPurchase == null)
                {
                    _logger.LogWarning("Purchase with ID: {PurchaseId} not found for update", id);
                    return NotFound();
                }
                _logger.LogInformation("Purchase {PurchaseId} updated successfully", id);
                return Ok(updatedPurchase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating purchase {PurchaseId}", id);
                return StatusCode(500, "Internal server error while updating purchase");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Delete purchase by Id from the database")]
        [SwaggerResponse(200, "Successfully deleted purchase!")]
        [SwaggerResponse(204, "Successfully deleted purchase, no additional content!")]
        [SwaggerResponse(400, "Bad request!")]
        [SwaggerResponse(500, "Something is wrong with the server!")]
        public async Task<IActionResult> DeletePurchase(Guid id)
        {
            _logger.LogInformation("Deleting purchase with ID: {PurchaseId}", id);
            try
            {
                var success = await _purchaseRepository.DeleteAsync(id);
                if (!success)
                {
                    _logger.LogWarning("Purchase with ID: {PurchaseId} not found for deletion", id);
                    return NotFound();
                }
                _logger.LogInformation("Purchase {PurchaseId} deleted successfully", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting purchase {PurchaseId}", id);
                return StatusCode(500, "Internal server error while deleting purchase");
            }
        }

        [HttpGet("buyer/{buyerId}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all purchases for a specific buyer")]
        [SwaggerResponse(200, "Successfully retrieved buyer purchases")]
        [SwaggerResponse(404, "Buyer not found")]
        public async Task<IActionResult> GetPurchasesByBuyer(Guid buyerId)
        {
            try
            {
                var purchases = await _purchaseRepository.GetPurchasesByBuyerIdAsync(buyerId);
                if (!purchases.Any())
                    return NotFound($"No purchases found for buyer with ID: {buyerId}");

                return Ok(purchases);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving purchases for buyer {BuyerId}", buyerId);
                return StatusCode(500, "Internal server error while retrieving buyer purchases");
            }
        }

        [HttpGet("owner/{ownerId}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get all purchases for a specific owner")]
        [SwaggerResponse(200, "Successfully retrieved owner purchases")]
        [SwaggerResponse(404, "Owner not found")]
        public async Task<IActionResult> GetPurchasesByOwner(Guid ownerId)
        {
            try
            {
                var purchases = await _purchaseRepository.GetPurchasesByOwnerIdAsync(ownerId);
                if (!purchases.Any())
                    return NotFound($"No purchases found for owner with ID: {ownerId}");

                return Ok(purchases);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving purchases for owner {OwnerId}", ownerId);
                return StatusCode(500, "Internal server error while retrieving owner purchases");
            }
        }
    }
}