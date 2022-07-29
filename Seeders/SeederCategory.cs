using IstimAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IstimAPI.Seeders
{
    public class SeederCategory: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasData(
                new Category { Id = 1, Title = "Ação"},
                new Category { Id = 2, Title = "Aventura"},
                new Category { Id = 3, Title = "Corrida"},
                new Category { Id = 4, Title = "FPS"},
                new Category { Id = 5, Title = "Luta"},
                new Category { Id = 6, Title = "RPG"},
                new Category { Id = 7, Title = "Simulador"},
                new Category { Id = 8, Title = "Estratégia"},
                new Category { Id = 9, Title = "Esporte"}
            );
        }
    }
}