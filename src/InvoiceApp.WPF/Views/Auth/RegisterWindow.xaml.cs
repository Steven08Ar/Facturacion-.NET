using InvoiceApp.WPF.ViewModels.Auth;
using System.Windows;

namespace InvoiceApp.WPF.Views.Auth;

public partial class RegisterWindow : Window
{
    private readonly RegisterViewModel _viewModel;

    public RegisterWindow(RegisterViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        _viewModel.Password = PasswordBox.Password;
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
