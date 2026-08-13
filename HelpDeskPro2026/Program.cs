using Microsoft.AspNetCore.Authentication.Cookies;
using HelpDeskPro2026.Data;
using HelpDeskPro2026.Data.Seed;
using HelpDeskPro2026.Interfaces;
using HelpDeskPro2026.Models.Configuration;
using HelpDeskPro2026.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.LogoutPath = "/Account/Logout";

        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

builder.Services.Configure<SupabaseSettings>(
    builder.Configuration.GetSection("Supabase"));

builder.Services.AddSingleton<SupabaseClientService>();

builder.Services.AddScoped<ISistemaService, SistemaService>();

builder.Services.AddScoped<ICategoriaService, CategoriaService>();

builder.Services.AddScoped<IRiesgoService, RiesgoService>();

builder.Services.AddScoped<IPrioridadService, PrioridadService>();

builder.Services.AddScoped<IEstadoService, EstadoService>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IStorageService, StorageService>();

builder.Services.AddScoped<ISupabaseAuthService, SupabaseAuthService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await context.Database.MigrateAsync();

    await DbInitializer.SeedAsync(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
