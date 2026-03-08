using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InvoiceApp.Application.Auth.Commands.Login;
using InvoiceApp.WPF.Services;
using MediatR;
using System.Windows;

namespace InvoiceApp.WPF.ViewModels.Auth;

public partial class LoginViewModel : ObservableObject
{
    private readonly IMediator _mediator;
    private readonly INavigationService _navigation;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _isLoading = false;

    public string Password { get; set; } = string.Empty;

    public LoginViewModel(IMediator mediator, INavigationService navigation)
    {
        _mediator = mediator;
        _navigation = navigation;
    }

    [RelayCommand(CanExecute = nameof(CanLogin))]
    private async Task LoginAsync()
    {
        ErrorMessage = string.Empty;
        IsLoading = true;

        try
        {
            var result = await _mediator.Send(new LoginQuery(Email, Password));

            if (result.IsSuccess)
            {
                _navigation.ShowMainWindow();
            }
            else
            {
                ErrorMessage = result.Error ?? "Credenciales inválidas.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error inesperado: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    private bool CanLogin() => !string.IsNullOrWhiteSpace(Email) && !IsLoading;

    [RelayCommand]
    private void GoToRegister()
    {
        _navigation.ShowRegisterWindow();
    }

    partial void OnEmailChanged(string value) => LoginCommand.NotifyCanExecuteChanged();
    partial void OnIsLoadingChanged(bool value) => LoginCommand.NotifyCanExecuteChanged();
}
