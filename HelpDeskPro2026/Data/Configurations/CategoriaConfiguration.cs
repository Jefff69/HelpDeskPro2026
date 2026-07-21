using HelpDeskPro2026.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskPro2026.Data.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");

            builder.Property(c => c.Nombre)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(c => c.Nombre)
                   .IsUnique();

            builder.Property(c => c.Activo)
       .HasDefaultValue(true);

        }
    }
}