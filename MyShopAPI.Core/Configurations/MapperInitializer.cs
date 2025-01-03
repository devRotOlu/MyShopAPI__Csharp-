using AutoMapper;
using MyShopAPI.Core.EntityDTO;
using MyShopAPI.Core.EntityDTO.CartDTO;
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
            CreateMap<SignUpDTO,Customer>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore()); ;
            CreateMap<CustomerDetails, CustomerDTO>().ReverseMap();
            CreateMap<CustomerDetails, ReviewerDTO>().ReverseMap();
            CreateMap<SignUpDTO, CustomerDetails>();
            CreateMap<CustomerDetails, DetailsDTO>().ReverseMap();
            CreateMap<CustomerDetails, CustomerDTO>().ReverseMap();
            CreateMap<CartAndWishlist, GetCartDTO>()
                      .ForMember(getCartDTO=>getCartDTO.CartQuantity,opt=>opt.MapFrom(cart=>cart.Quantity));
            CreateMap<CartAndWishlist, AddWishlistDTO>().ReverseMap();
            CreateMap<CartAndWishlist, AddCartDTO>().ReverseMap();
            CreateMap<CartAndWishlist, GetWishlistDTO>();
            CreateMap<Product, AddProductDTO>().ReverseMap();
            CreateMap<Product, GetProductDTO>().ReverseMap();
            CreateMap<ProductImage, ImageDTO>().ReverseMap();
            CreateMap<ProductReview,AddReviewDTO>().ReverseMap();
            CreateMap<ProductReview,ReviewDTO>().ReverseMap();
        }
    }
}
