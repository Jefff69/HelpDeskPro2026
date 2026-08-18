using HelpDeskPro2026.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskPro2026.Data.Configurations
{
    public class RiesgoConfiguration : IEntityTypeConfiguration<Riesgo>
    {
        public void Configure(EntityTypeBuilder<Riesgo> builder)
        {
            builder.ToTable("Riesgos");

            builder.Property(r => r.Nombre)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(r => r.Color)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasIndex(r => r.Nombre)
                   .IsUnique();
        }
    }
}