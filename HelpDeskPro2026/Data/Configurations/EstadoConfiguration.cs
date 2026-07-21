using HelpDeskPro2026.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskPro2026.Data.Configurations
{
    public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("Estados");

            builder.Property(e => e.Nombre)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(e => e.Nombre)
                   .IsUnique();

            builder.Property(e => e.Activo)
       .HasDefaultValue(true);
        }
    }
}