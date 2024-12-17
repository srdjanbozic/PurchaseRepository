using AutoMapper;
using Purchase_Microservice.Application.Dtos;
using Purchase_Microservice.Domain.Entities;

namespace Purchase_Microservice.Application.MapperProfiles
{
    public class PurchaseProfile : Profile
    {
        public PurchaseProfile()
        {
            // Mapping from PurchaseCreateDto to Purchase entity
            CreateMap<PurchaseCreateDto, Purchase>()
                .ForMember(dest => dest.PurchaseId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Buyer, opt => opt.MapFrom(src =>
                    new Buyer(src.BuyerId, src.BuyerUsername, src.BuyerEmail)))
                .ForMember(dest => dest.Delivery, opt => opt.MapFrom(src =>
                    new Delivery(src.DeliveryId, src.DeliveryPrice)))
                .ForMember(dest => dest.Post, opt => opt.MapFrom(src =>
                    new Post(src.PostId, src.PostTitle, src.PostDate, src.PostPrice,
                        src.OwnerId, src.OwnerName, src.OwnerEmail, src.OwnerPhone)));

            // Mapping from Purchase entity to PurchaseReadDto
            CreateMap<Purchase, PurchaseReadDto>()
                .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.Buyer.BuyerId))
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Post.PostId))
                .ForMember(dest => dest.DeliveryId, opt => opt.MapFrom(src => src.Delivery.DeliveryId))
                .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.Buyer.BuyerId))
                .ForMember(dest => dest.BuyerUsername, opt => opt.MapFrom(src => src.Buyer.BuyerUsername))
                .ForMember(dest => dest.BuyerEmail, opt => opt.MapFrom(src => src.Buyer.BuyerEmail))
                .ForMember(dest => dest.DeliveryId, opt => opt.MapFrom(src => src.Delivery.DeliveryId))
                .ForMember(dest => dest.DeliveryPrice, opt => opt.MapFrom(src => src.Delivery.DeliveryPrice))
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Post.PostId))
                .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(src => src.Post.PostTitle))
                .ForMember(dest => dest.PostDate, opt => opt.MapFrom(src => src.Post.CreatedDate))
                .ForMember(dest => dest.PostPrice, opt => opt.MapFrom(src => src.Post.Price))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.Post.OwnerId))
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Post.OwnerName))
                .ForMember(dest => dest.OwnerEmail, opt => opt.MapFrom(src => src.Post.OwnerEmail))
                .ForMember(dest => dest.OwnerPhone, opt => opt.MapFrom(src => src.Post.OwnerPhone));

            // Mapping from PurchaseUpdateDto to Purchase entity
            CreateMap<PurchaseUpdateDto, Purchase>();
        }
    }
}