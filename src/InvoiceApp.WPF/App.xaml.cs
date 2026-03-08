using InvoiceApp.Application;
using InvoiceApp.Infrastructure;
using InvoiceApp.WPF.Services;
using InvoiceApp.WPF.ViewModels.Auth;
using InvoiceApp.WPF.ViewModels.Shell;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace InvoiceApp.WPF;

/// <summary>
/// Punto de entrada de la aplicación WPF.
/// Usamos OnStartup() en lugar de StartupUri en el XAML para controlar
/// la inyección de dependencias con IHost.
/// </summary>
public partial class App : System.Windows.Application
{
    private IHost _host = null!;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // ── Capas de negocio ──────────────────────────────────────
                services.AddApplicationServices();           // MediatR, FluentValidation
                services.AddInfrastructureServices(context.Configuration); // EF Core, Identity

                // ── Navegación ────────────────────────────────────────────
                services.AddSingleton<INavigationService, NavigationService>();

                // ── ViewModels ────────────────────────────────────────────
                services.AddTransient<LoginViewModel>();
                services.AddTransient<RegisterViewModel>();
                services.AddSingleton<MainViewModel>();

                // ── Ventanas WPF ──────────────────────────────────────────
                services.AddTransient<Views.Auth.LoginWindow>();
                services.AddTransient<Views.Auth.RegisterWindow>();
                services.AddSingleton<Views.Shell.MainWindow>();
            })
            .Build();

        await _host.StartAsync();

        // Crear/verificar base de datos al iniciar
        using (var scope = _host.Services.CreateScope())
        {
            var db = scope.ServiceProvider
                .GetRequiredService<InvoiceApp.Infrastructure.Persistence.ApplicationDbContext>();
            await db.Database.EnsureCreatedAsync();
        }

        // Mostrar ventana de login
        var loginWindow = _host.Services.GetRequiredService<Views.Auth.LoginWindow>();
        loginWindow.Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();
        base.OnExit(e);
    }

    /// <summary>Obtiene un servicio mediante el DI container global.</summary>
    public static T GetService<T>() where T : class
        => ((App)Current)._host.Services.GetRequiredService<T>();
}
