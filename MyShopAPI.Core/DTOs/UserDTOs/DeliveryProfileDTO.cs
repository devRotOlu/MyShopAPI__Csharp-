﻿namespace MyShopAPI.Core.DTOs.UserDTOs
{
    public class DeliveryProfileDTO:AddDeliveryProfileDTO
    {
        public int Id { get; set; }
        public bool? IsDefaultProfile { get; set; }
    }
}
