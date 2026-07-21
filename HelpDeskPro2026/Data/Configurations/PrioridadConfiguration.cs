using HelpDeskPro2026.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskPro2026.Data.Configurations
{
    public class PrioridadConfiguration : IEntityTypeConfiguration<Prioridad>
    {
        public void Configure(EntityTypeBuilder<Prioridad> builder)
        {
            builder.ToTable("Prioridades");

            builder.Property(p => p.Nombre)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Color)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasIndex(p => p.Nombre)
                   .IsUnique();
        }
    }
}