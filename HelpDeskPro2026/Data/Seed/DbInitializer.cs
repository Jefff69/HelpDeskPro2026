using HelpDeskPro2026.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskPro2026.Data.Seed
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context)
        {
             string[] roles =
                        {
                "Super Usuario",
                "Técnico",
                "Usuario"
            };

            foreach (var nombreRol in roles)
            {
                bool existe = await context.Roles
                    .AnyAsync(r => r.Nombre == nombreRol);

                if (!existe)
                {
                    context.Roles.Add(new Rol
                    {
                        Nombre = nombreRol
                    });
                }
            }

            await context.SaveChangesAsync();


            bool existeAdmin = await context.Usuarios
                .AnyAsync(u => u.Correo == "admin@helpdesk.com");

            if (!existeAdmin)
            {
                var rolAdmin = await context.Roles
                    .FirstAsync(r => r.Nombre == "Super Usuario");

                context.Usuarios.Add(new Usuario
                {
                    Nombre = "Administrador",
                    Apellidos = "Sistema",
                    Correo = "admin@helpdesk.com",
                    Activo = true,
                    FechaCreacion = DateTime.UtcNow,
                    RolId = rolAdmin.Id
                });

                await context.SaveChangesAsync();
            }



        }
    }
}