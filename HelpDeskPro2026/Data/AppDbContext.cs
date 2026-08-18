using HelpDeskPro2026.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskPro2026.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Usuarios y roles
        public DbSet<Rol> Roles { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }


        // Catálogos
        public DbSet<Sistema> Sistemas { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Riesgo> Riesgos { get; set; }

        public DbSet<Prioridad> Prioridades { get; set; }

        public DbSet<Estado> Estados { get; set; }


        // Incidencias
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Comentario> Comentarios { get; set; }

        public DbSet<Adjunto> Adjuntos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplica las configuraciones existentes
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);


            // ==========================================
            // USUARIO → ROL
            // ==========================================

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict);


            // ==========================================
            // TICKET → SOLICITANTE
            // ==========================================

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Solicitante)
                .WithMany(u => u.TicketsCreados)
                .HasForeignKey(t => t.SolicitanteId)
                .OnDelete(DeleteBehavior.Restrict);


            // ==========================================
            // TICKET → TÉCNICO ASIGNADO
            // ==========================================

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.TecnicoAsignado)
                .WithMany(u => u.TicketsAsignados)
                .HasForeignKey(t => t.TecnicoAsignadoId)
                .OnDelete(DeleteBehavior.SetNull);


            // ==========================================
            // TICKET → SISTEMA
            // ==========================================

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Sistema)
                .WithMany()
                .HasForeignKey(t => t.SistemaId)
                .OnDelete(DeleteBehavior.Restrict);


            // ==========================================
            // TICKET → CATEGORÍA
            // ==========================================

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Categoria)
                .WithMany()
                .HasForeignKey(t => t.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);


            // ==========================================
            // TICKET → RIESGO
            // ==========================================

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Riesgo)
                .WithMany()
                .HasForeignKey(t => t.RiesgoId)
                .OnDelete(DeleteBehavior.Restrict);


            // ==========================================
            // TICKET → PRIORIDAD
            // ==========================================

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Prioridad)
                .WithMany()
                .HasForeignKey(t => t.PrioridadId)
                .OnDelete(DeleteBehavior.Restrict);


            // ==========================================
            // TICKET → ESTADO
            // ==========================================

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Estado)
                .WithMany()
                .HasForeignKey(t => t.EstadoId)
                .OnDelete(DeleteBehavior.Restrict);


            // ==========================================
            // TICKET → COMENTARIOS
            // ==========================================

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Ticket)
                .WithMany(t => t.Comentarios)
                .HasForeignKey(c => c.TicketId)
                .OnDelete(DeleteBehavior.Cascade);


            // ==========================================
            // COMENTARIO → USUARIO
            // ==========================================

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Comentarios)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);


            // ==========================================
            // TICKET → ADJUNTOS
            // ==========================================

            modelBuilder.Entity<Adjunto>()
                .HasOne(a => a.Ticket)
                .WithMany(t => t.ArchivosAdjuntos)
                .HasForeignKey(a => a.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}