using MyShopAPI.Core.EntityDTO.UserDTO;

namespace MyShopAPI.Core.EntityDTO.ProoductDTO
{
    public class GetProductDTO : BaseGetProductDTO
    {
        public ICollection<ReviewerDTO> Reviews { get; set; } = null!;
    }
}
