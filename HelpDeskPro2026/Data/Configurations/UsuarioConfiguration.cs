using HelpDeskPro2026.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskPro2026.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.Property(u => u.Nombre)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.Apellidos)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.Correo)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.SupabaseUserId)
                   .HasMaxLength(100);

            builder.Property(u => u.FotoUrl)
                   .HasMaxLength(255);

            builder.HasIndex(u => u.Correo)
                   .IsUnique();

            builder.Property(u => u.Activo)
       .HasDefaultValue(true);

            builder.Property(u => u.FechaCreacion)
       .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}