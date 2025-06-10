using AutoMapper;
using MyShopAPI.Core.DTOs;
using MyShopAPI.Core.DTOs.CartDTOs;
using MyShopAPI.Core.DTOs.CategoryDTOs;
using MyShopAPI.Core.DTOs.OrderDTOs;
using MyShopAPI.Core.DTOs.ProductAttributeDTOs;
using MyShopAPI.Core.DTOs.ProductDTOs;
using MyShopAPI.Core.DTOs.ProductReviewDTOs;
using MyShopAPI.Core.DTOs.UserDTOs;
using MyShopAPI.Core.DTOs.WishlistDTOs;
using MyShopAPI.Core.EntityDTO.WishlistDTOs;
using MyShopAPI.Data.Entities;
using OtherAttribute = MyShopAPI.Data.Entities.Attribute;

namespace MyShopAPI.Core.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<SignUpDTO, Customer>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore());
            CreateMap<CustomerDetails, CustomerDTO>().ReverseMap();
            CreateMap<Customer, ReviewerDTO>().ReverseMap();
            CreateMap<SignUpDTO, CustomerDetails>();
            CreateMap<CustomerDetails, CustomerDetailsDTO>().ReverseMap();
            CreateMap<CustomerDetails, BaseUserDetailsDTO>().ReverseMap();
            CreateMap<Cart, GetCartDTO>()
                      .ForMember(getCartDTO => getCartDTO.CartQuantity, opt => opt.MapFrom(cart => cart.Quantity));
            CreateMap<Wishlist, AddWishlistDTO>()
                .ReverseMap()
                .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<Cart, AddCartDTO>()
                .ReverseMap()
                .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<Wishlist, UpdateCartDTO>().ReverseMap();
            CreateMap<Wishlist, GetWishlistDTO>();
            CreateMap<Product, AddProductDTO>().ReverseMap();
            CreateMap<GetProductDTO, Product>().ReverseMap();
            CreateMap<Product, BaseGetProductDTO>().ReverseMap();
            CreateMap<ProductImage, ImageDTO>().ReverseMap();
            CreateMap<ProductReview, AddReviewDTO>().ReverseMap();
            CreateMap<ProductReview, ReviewDTO>().ReverseMap();
            CreateMap<DeliveryProfile, AddDeliveryProfileDTO>().ReverseMap();
            CreateMap<DeliveryProfile, DeliveryProfileDTO>().ReverseMap();
            CreateMap<CustomerOrder, GetOrderDTO>()
                .ForMember(orderDTO => orderDTO.OrderedItems, opt => opt.MapFrom(order => order.CartItems));
            CreateMap<CustomerOrder, AddOrderDTO>().ReverseMap();
            CreateMap<CartOrder, CartOrderDTO>().ReverseMap();
            CreateMap<Category, GetCategoryDTO>().ReverseMap();
            CreateMap<ProductAttributeDTO, ProductAttribute>().ReverseMap();
            CreateMap<IEnumerable<ProductAttributeDTO>, Product>()
                .ForMember(product => product.Attributes, opt => opt.MapFrom(attributes => attributes));
            CreateMap<AttributeDTO, OtherAttribute>().ReverseMap();
        }
    }
}
