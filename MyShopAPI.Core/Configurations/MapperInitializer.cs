using AutoMapper;
using MyShopAPI.Core.EntityDTO;
using MyShopAPI.Core.EntityDTO.CartDTO;
using MyShopAPI.Core.EntityDTO.OrderDTO;
using MyShopAPI.Core.EntityDTO.ProductReviewDTO;
using MyShopAPI.Core.EntityDTO.ProoductDTO;
using MyShopAPI.Core.EntityDTO.UserDTO;
using MyShopAPI.Core.EntityDTO.WishlistDTO;
using MyShopAPI.Data.Entities;

namespace MyShopAPI.Core.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<SignUpDTO, Customer>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore()); ;
            CreateMap<CustomerDetails, CustomerDTO>().ReverseMap();
            CreateMap<CustomerDetails, ReviewerDTO>().ReverseMap();
            CreateMap<SignUpDTO, CustomerDetails>();
            CreateMap<CustomerDetails, CustomerDetailsDTO>().ReverseMap();
            CreateMap<Cart, GetCartDTO>()
                      .ForMember(getCartDTO => getCartDTO.CartQuantity, opt => opt.MapFrom(cart => cart.Quantity));
            CreateMap<Wishlist, AddWishlistDTO>().ReverseMap();
            CreateMap<Cart, AddCartDTO>().ReverseMap();
            CreateMap<Wishlist, UpdateCartDTO>().ReverseMap();
            CreateMap<Wishlist, GetWishlistDTO>();
            CreateMap<Product, AddProductDTO>().ReverseMap();
            CreateMap<Product, GetProductDTO>().ReverseMap();
            CreateMap<Product, BaseGetProductDTO>().ReverseMap();
            CreateMap<ProductImage, ImageDTO>().ReverseMap();
            CreateMap<ProductReview, AddReviewDTO>().ReverseMap();
            CreateMap<ProductReview, ReviewDTO>().ReverseMap();
            CreateMap<DeliveryProfile,AddDeliveryProfileDTO>().ReverseMap();
            CreateMap<DeliveryProfile, DeliveryProfileDTO>().ReverseMap();
            CreateMap<CustomerOrder,GetOrderDTO>().ReverseMap();
            CreateMap<CustomerOrder, AddOrderDTO>().ReverseMap();
        }
    }
}
