using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EFCoreCodeFirstSample.Data;

namespace EFCoreCodeFirstSample.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Helotn",
                    Address = "Olaya",
                    CountryId = 1,
                    Rating = 4.5
                });
        }
    }
}
