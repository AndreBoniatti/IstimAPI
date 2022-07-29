using IstimAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IstimAPI.Seeders
{
    public class SeederAgeRange : IEntityTypeConfiguration<AgeRange>
    {
        public void Configure(EntityTypeBuilder<AgeRange> builder)
        {
            builder.ToTable("AgeRanges");

            builder.HasData(
                new AgeRange { Id = 1, Title = "L", Range = 0 },
                new AgeRange { Id = 2, Title = "10+", Range = 10 },
                new AgeRange { Id = 3, Title = "12+", Range = 12 },
                new AgeRange { Id = 4, Title = "14+", Range = 14 },
                new AgeRange { Id = 5, Title = "16+", Range = 16 },
                new AgeRange { Id = 6, Title = "18+", Range = 18 }
            );
        }
    }
}