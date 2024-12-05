using AutoMapper;
using Microservice_RatingService.Application.Dtos;
using Microservice_RatingService.Domain.Entities;

namespace Microservice_RatingService.Application.MapperProfiles
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            // Mapping from RatingCreateDto to Rating entity
            CreateMap<RatingCreateDto, Rating>()
            .ForMember(dest => dest.RatingId, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Purchase, opt => opt.MapFrom(src =>
            new Purchase(src.PurchaseId, src.PurchaseDate ?? DateTime.MinValue, src.PurchasePrice ?? 0m)))
            .ForMember(dest => dest.Buyer, opt => opt.MapFrom(src =>
             new Buyer(src.BuyerId, "defaultBuyerUsername", "defaultBuyer@email.com")))
            .ForMember(dest => dest.Seller, opt => opt.MapFrom(src =>
            new Seller(src.SellerId, "defaultSellerUsername", "defaultSeller@email.com")));

            // Mapping from Rating entity to RatingReadDto
            CreateMap<Rating, RatingReadDto>()
                .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.Buyer.BuyerId))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.Seller.SellerId))
                .ForMember(dest => dest.PurchaseId, opt => opt.MapFrom(src => src.Purchase.PurchaseId));

            // Mapping from RatingUpdateDto to Rating entity
            CreateMap<RatingUpdateDto, Rating>();

        }
    }
}
