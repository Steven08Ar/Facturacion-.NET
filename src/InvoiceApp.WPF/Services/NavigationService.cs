using System.Windows;

namespace InvoiceApp.WPF.Services;

public interface INavigationService
{
    void ShowMainWindow();
    void ShowLoginWindow();
    void ShowRegisterWindow();
}

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void ShowMainWindow()
    {
        var mainWindow = (Views.Shell.MainWindow)_serviceProvider.GetService(typeof(Views.Shell.MainWindow))!;

        foreach (Window w in System.Windows.Application.Current.Windows)
        {
            if (w is not Views.Shell.MainWindow)
                w.Close();
        }

        mainWindow.Show();
        System.Windows.Application.Current.MainWindow = mainWindow;
    }

    public void ShowLoginWindow()
    {
        var loginWindow = (Views.Auth.LoginWindow)_serviceProvider.GetService(typeof(Views.Auth.LoginWindow))!;
        loginWindow.Show();
        System.Windows.Application.Current.MainWindow = loginWindow;
    }

    public void ShowRegisterWindow()
    {
        var registerWindow = (Views.Auth.RegisterWindow)_serviceProvider.GetService(typeof(Views.Auth.RegisterWindow))!;
        registerWindow.ShowDialog();
    }
}
