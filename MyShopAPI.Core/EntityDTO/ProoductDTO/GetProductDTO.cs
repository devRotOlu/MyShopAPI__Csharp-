namespace MyShopAPI.Core.EntityDTO.ProoductDTO
{
    public class GetProductDTO : ProductDTO
    {
        public int Id { get; set; }
        public ICollection<ImageDTO> Images { get; set; } = null!;
    }
}
