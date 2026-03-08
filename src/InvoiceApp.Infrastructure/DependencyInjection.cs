using InvoiceApp.Application.Common.Interfaces;
using InvoiceApp.Domain.Entities;
using InvoiceApp.Infrastructure.Identity;
using InvoiceApp.Infrastructure.Persistence;
using InvoiceApp.Infrastructure.Persistence.Interceptors;
using InvoiceApp.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace InvoiceApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntityInterceptor>();
        services.AddScoped<SoftDeleteInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            // SQLite: no requiere SQL Server instalado, la BD se crea
            // automáticamente como archivo .db junto al ejecutable.
            var dbPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "InvoiceApp.db");
            options.UseSqlite($"Data Source={dbPath}",
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        // Identity Registration
        services.AddIdentity<User, IdentityRole<Guid>>(options => 
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        // JWT config
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
