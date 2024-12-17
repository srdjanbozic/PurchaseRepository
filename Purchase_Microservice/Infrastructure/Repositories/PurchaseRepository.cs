// PurchaseRepository.cs
using AutoMapper;
using Purchase_Microservice.Application.Dtos;
using Purchase_Microservice.Application.Exceptions;
using Purchase_Microservice.Domain.Entities;
using Purchase_Microservice.Domain.Interfaces;
using Purchase_Microservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Purchase_Microservice.Application.Exceptions.Purchase_Microservice.Application.Exceptions;

namespace Purchase_Microservice.Infrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PurchaseRepository> _logger;

        public PurchaseRepository(AppDbContext context, IMapper mapper, ILogger<PurchaseRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PurchaseReadDto> CreateAsync(PurchaseCreateDto purchaseCreateDto)
        {
            var purchase = _mapper.Map<Purchase>(purchaseCreateDto);
            await _context.Purchases.AddAsync(purchase);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Created purchase with ID: {PurchaseId}", purchase.PurchaseId);
            return _mapper.Map<PurchaseReadDto>(purchase);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
            {
                throw new PurchaseNotFoundException(id);
            }

            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Deleted purchase with ID: {PurchaseId}", id);
            return true;
        }

        public async Task<IEnumerable<PurchaseReadDto>> GetAllAsync()
        {
            var purchases = await _context.Purchases.ToListAsync();
            _logger.LogInformation("Retrieved {Count} purchases from database", purchases.Count);
            return _mapper.Map<IEnumerable<PurchaseReadDto>>(purchases);
        }

        public async Task<PurchaseReadDto> GetByIdAsync(Guid id)
        {
            var purchase = await _context.Purchases.FirstOrDefaultAsync(p => p.PurchaseId == id);
            if (purchase == null)
            {
                _logger.LogWarning("Purchase with ID {PurchaseId} not found", id);
                throw new PurchaseNotFoundException(id);
            }

            _logger.LogInformation("Retrieved purchase with ID: {PurchaseId}", id);
            return _mapper.Map<PurchaseReadDto>(purchase);
        }

        public async Task<PurchaseReadDto> UpdateAsync(Guid id, PurchaseUpdateDto purchaseUpdateDto)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
            {
                throw new PurchaseNotFoundException(id);
            }

            _mapper.Map(purchaseUpdateDto, purchase);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Updated purchase with ID: {PurchaseId}", id);
            return _mapper.Map<PurchaseReadDto>(purchase);
        }

        public async Task<IEnumerable<PurchaseReadDto>> GetPurchasesByBuyerIdAsync(Guid buyerId)
        {
            var purchases = await _context.Purchases
                .Where(p => p.Buyer.BuyerId == buyerId)
                .ToListAsync();

            _logger.LogInformation("Retrieved {Count} purchases for buyer {BuyerId}",
                purchases.Count, buyerId);
            return _mapper.Map<IEnumerable<PurchaseReadDto>>(purchases);
        }

        public async Task<IEnumerable<PurchaseReadDto>> GetPurchasesByOwnerIdAsync(Guid ownerId)
        {
            var purchases = await _context.Purchases
                .Where(p => p.Post.OwnerId == ownerId)
                .ToListAsync();

            _logger.LogInformation("Retrieved {Count} purchases for owner {OwnerId}",
                purchases.Count, ownerId);
            return _mapper.Map<IEnumerable<PurchaseReadDto>>(purchases);
        }
    }
}