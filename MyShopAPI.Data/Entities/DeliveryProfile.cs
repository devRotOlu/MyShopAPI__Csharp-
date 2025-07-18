﻿using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Data.Entities
{
    public class DeliveryProfile : BaseCustomer
    {
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string StreetAddress { get; set; } = null!;
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string State { get; set; } = null!;
        [Required]
        public string LGA { get; set; } = null!;
        public string? Directions { get; set; }
        public bool? IsDefaultProfile { get; set; }
        public bool? IsDeleted { get; set; }
        public ICollection<CustomerOrder> Orders { get; set; } = null!;
    }
}
