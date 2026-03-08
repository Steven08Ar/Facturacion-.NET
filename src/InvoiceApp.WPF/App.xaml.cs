using InvoiceApp.Application;
using InvoiceApp.Infrastructure;
using InvoiceApp.WPF.Services;
using InvoiceApp.WPF.ViewModels.Auth;
using InvoiceApp.WPF.ViewModels.Shell;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Windows;

namespace InvoiceApp.WPF;

public partial class App : System.Windows.Application
{
    private IHost _host = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        // ── Captura global de excepciones no manejadas ───────────────────
        DispatcherUnhandledException += (_, ex) =>
        {
            LogError(ex.Exception);
            MessageBox.Show(
                $"Error inesperado:\n\n{ex.Exception.Message}\n\nRevisa el archivo 'error.log' junto al .exe para más detalle.",
                "InvoiceApp — Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            ex.Handled = true;
        };

        AppDomain.CurrentDomain.UnhandledException += (_, ex) =>
        {
            if (ex.ExceptionObject is Exception exc)
                LogError(exc);
        };

        base.OnStartup(e);

        try
        {
            StartApp();
        }
        catch (Exception ex)
        {
            LogError(ex);
            MessageBox.Show(
                $"No se pudo iniciar InvoiceApp:\n\n{ex.Message}",
                "InvoiceApp — Error al iniciar",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            Shutdown(1);
        }
    }

    private void StartApp()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddApplicationServices();
                services.AddInfrastructureServices(context.Configuration);

                services.AddSingleton<INavigationService, NavigationService>();

                services.AddTransient<LoginViewModel>();
                services.AddTransient<RegisterViewModel>();
                services.AddSingleton<MainViewModel>();

                services.AddTransient<Views.Auth.LoginWindow>();
                services.AddTransient<Views.Auth.RegisterWindow>();
                services.AddSingleton<Views.Shell.MainWindow>();
            })
            .Build();

        _host.Start();

        // Preparar BD (si LocalDB no está disponible, esto lanzará excepción visible)
        using var scope = _host.Services.CreateScope();
        var db = scope.ServiceProvider
            .GetRequiredService<InvoiceApp.Infrastructure.Persistence.ApplicationDbContext>();

        // Sólo crea la BD si no existe — no necesita LocalDB si ya tiene SQL Server
        db.Database.EnsureCreated();

        var loginWindow = _host.Services.GetRequiredService<Views.Auth.LoginWindow>();
        loginWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _host?.StopAsync().Wait(TimeSpan.FromSeconds(5));
        _host?.Dispose();
        base.OnExit(e);
    }

    private static void LogError(Exception ex)
    {
        try
        {
            var log = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "error.log");
            File.AppendAllText(log,
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {ex}\n\n");
        }
        catch { /* no hacer nada si el log también falla */ }
    }

    public static T GetService<T>() where T : class
        => ((App)Current)._host.Services.GetRequiredService<T>();
}
