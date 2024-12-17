using Purchase_Microservice.Application.Dtos;

namespace Purchase_Microservice.Domain.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<PurchaseReadDto> CreateAsync(PurchaseCreateDto purchaseCreateDto);
        Task<IEnumerable<PurchaseReadDto>> GetAllAsync();
        Task<PurchaseReadDto> GetByIdAsync(Guid id);
        Task<PurchaseReadDto> UpdateAsync(Guid id, PurchaseUpdateDto purchaseUpdateDto);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<PurchaseReadDto>> GetPurchasesByBuyerIdAsync(Guid buyerId);
        Task<IEnumerable<PurchaseReadDto>> GetPurchasesByOwnerIdAsync(Guid ownerId);
    }
}
