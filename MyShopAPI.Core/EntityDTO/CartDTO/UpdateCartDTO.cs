using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.EntityDTO.CartDTO
{
    public class UpdateCartDTO:AddCartDTO
    {
        [Required, Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}
