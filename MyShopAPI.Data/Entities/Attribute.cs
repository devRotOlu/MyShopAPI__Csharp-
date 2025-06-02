using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Data.Entities
{
    public class Attribute
    {
        public int Id { get; set; }   
        [Required,MinLength(1)]
        public string Name { get; set; } = null!;
        public string? Unit { get; set; }
    }
}
