using InvoiceApp.WPF.ViewModels.Auth;
using System.Windows;
using System.Windows.Controls;

namespace InvoiceApp.WPF.Views.Auth;

public partial class LoginWindow : Window
{
    private readonly LoginViewModel _viewModel;

    public LoginWindow(LoginViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        _viewModel.Password = PasswordBox.Password;
    }
}
