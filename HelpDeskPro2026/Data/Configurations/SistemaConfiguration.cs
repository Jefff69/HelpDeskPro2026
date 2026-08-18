using HelpDeskPro2026.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskPro2026.Data.Configurations
{
    public class SistemaConfiguration : IEntityTypeConfiguration<Sistema>
    {
        public void Configure(EntityTypeBuilder<Sistema> builder)
        {
            builder.ToTable("Sistemas");

            builder.Property(s => s.Codigo)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(s => s.Nombre)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(s => s.Codigo)
                   .IsUnique();

            builder.HasIndex(s => s.Nombre)
                   .IsUnique();

            builder.Property(s => s.Activo)
             .HasDefaultValue(true);
        }
    }
}