using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EFCoreCodeFirstSample.Data;

namespace EFCoreCodeFirstSample.Configurations.Entities
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                            new Country
                            {
                                Id = 1,
                                Name = "Saudi Arabia",
                                ShortName = "SA"
                            });
        }
    }
}
