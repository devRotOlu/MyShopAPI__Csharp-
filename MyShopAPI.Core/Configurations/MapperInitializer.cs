using AutoMapper;
using MyShopAPI.Core.EntityDTO;
using MyShopAPI.Core.EntityDTO.CartDTO;
using MyShopAPI.Core.EntityDTO.ProductReviewDTO;
using MyShopAPI.Core.EntityDTO.ProoductDTO;
using MyShopAPI.Core.EntityDTO.UserDTO;
using MyShopAPI.Data.Entities;

namespace MyShopAPI.Core.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Customer, SignUpDTO>().ReverseMap();
            CreateMap<Customer, LoginDTO>().ReverseMap();
            CreateMap<CustomerDetails, CustomerDTO>().ReverseMap();
            CreateMap<CustomerDetails, ReviewerDTO>().ReverseMap();
            CreateMap<CustomerDetails, SignUpDTO>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore());
            CreateMap<CustomerDetails, DetailsDTO>().ReverseMap();
            CreateMap<CustomerDetails, CustomerDTO>().ReverseMap();
            CreateMap<Cart, GetCartDTO>()
                      .ForMember(getCartDTO=>getCartDTO.CartQuantity,opt=>opt.MapFrom(cart=>cart.Quantity));
            CreateMap<Cart, AddCartDTO>().ReverseMap();
            CreateMap<Product, AddProductDTO>().ReverseMap();
            CreateMap<Product, GetProductDTO>().ReverseMap();
            CreateMap<ProductImage, ImageDTO>().ReverseMap();
            CreateMap<ProductReview,AddReviewDTO>().ReverseMap();
            CreateMap<ProductReview,ReviewDTO>().ReverseMap();
            CreateMap<TokenDTO,RefreshToken>();
        }
    }
}
