using HelpDeskPro2026.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskPro2026.Data.Configurations
{
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("Roles");

            builder.Property(r => r.Nombre)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasIndex(r => r.Nombre)
                   .IsUnique();

            builder.HasData(
                new Rol
                {
                    Id = 1,
                    Nombre = "Empleado"
                },
                new Rol
                {
                    Id = 2,
                    Nombre = "Soporte Técnico"
                }
            );
        }
    }
}