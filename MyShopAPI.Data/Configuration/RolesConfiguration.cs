using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShopAPI.Data.Entities;
using System.Drawing;
using System.Reflection.Emit;

namespace MyShopAPI.Data.Configuration
{
    public class RolesConfiguration: IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "customer",
                    NormalizedName = "CUSTOMER"
                }
            );

            builder.HasData(
               new IdentityRole
               {
                   Name = "Administrator",
                   NormalizedName = "ADMINISTRATOR"
               }
           );
        }
    }
}
