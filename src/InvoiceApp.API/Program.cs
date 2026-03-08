using InvoiceApp.API.Middlewares;
using InvoiceApp.Application;
using InvoiceApp.Infrastructure;
using InvoiceApp.Infrastructure.Persistence;
using Serilog;

namespace InvoiceApp.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add Serilog
        builder.Host.UseSerilog((context, loggerConfig) => loggerConfig
            .ReadFrom.Configuration(context.Configuration));

        // Add Configuration
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                             .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                             .AddEnvironmentVariables();

        // Add Core Services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add Application and Infrastructure Services
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        // Add CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            // Note: Since this is just module 1, we will configure the proper JWT validation 
            // when we implement the Auth Module (Module 2).
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        // Auto-migrate and Seed Initial Data
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                // Only for development/local environment:
                // await context.Database.MigrateAsync();
                
                // Seed data (Requires UserManager/RoleManager to be injected in future)
                // For now, in Module 1, we will just ensure DB is created if using localDB.
                await context.Database.EnsureCreatedAsync(); 
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during database migration/seeding.");
            }
        }

        await app.RunAsync();
    }
}
