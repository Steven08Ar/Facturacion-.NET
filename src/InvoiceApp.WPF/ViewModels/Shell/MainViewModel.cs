using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InvoiceApp.WPF.Services;

namespace InvoiceApp.WPF.ViewModels.Shell;

public partial class MainViewModel : ObservableObject
{
    private readonly INavigationService _navigation;

    [ObservableProperty]
    private string _currentUser = "Administrador";

    [ObservableProperty]
    private string _currentModule = "Dashboard";

    [ObservableProperty]
    private object? _currentView;

    public MainViewModel(INavigationService navigation)
    {
        _navigation = navigation;
    }

    [RelayCommand]
    private void Logout()
    {
        _navigation.ShowLoginWindow();
    }

    [RelayCommand]
    private void NavigateToDashboard() => CurrentModule = "Dashboard";

    [RelayCommand]
    private void NavigateToCustomers() => CurrentModule = "Clientes";

    [RelayCommand]
    private void NavigateToInvoices() => CurrentModule = "Facturas";

    [RelayCommand]
    private void NavigateToProducts() => CurrentModule = "Productos";

    [RelayCommand]
    private void NavigateToPayments() => CurrentModule = "Pagos";

    [RelayCommand]
    private void NavigateToReports() => CurrentModule = "Reportes";

    [RelayCommand]
    private void NavigateToSettings() => CurrentModule = "Configuración";
}
