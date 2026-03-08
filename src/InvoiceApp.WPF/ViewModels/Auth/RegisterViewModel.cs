using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InvoiceApp.Application.Auth.Commands.RegisterCompany;
using InvoiceApp.WPF.Services;
using MediatR;
using System.Windows;

namespace InvoiceApp.WPF.ViewModels.Auth;

public partial class RegisterViewModel : ObservableObject
{
    private readonly IMediator _mediator;

    [ObservableProperty] private string _companyName = string.Empty;
    [ObservableProperty] private string _taxId = string.Empty;
    [ObservableProperty] private string _adminFullName = string.Empty;
    [ObservableProperty] private string _adminEmail = string.Empty;
    [ObservableProperty] private string _errorMessage = string.Empty;
    [ObservableProperty] private string _successMessage = string.Empty;
    [ObservableProperty] private bool _isLoading = false;

    public string Password { get; set; } = string.Empty;

    public RegisterViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    [RelayCommand]
    private async Task RegisterAsync()
    {
        ErrorMessage = string.Empty;
        SuccessMessage = string.Empty;
        IsLoading = true;

        try
        {
            var result = await _mediator.Send(
                new RegisterCompanyCommand(CompanyName, TaxId, AdminEmail, AdminFullName, Password));

            if (result.IsSuccess)
            {
                SuccessMessage = "✅ Empresa registrada exitosamente. Ya puede iniciar sesión.";
                CompanyName = TaxId = AdminFullName = AdminEmail = string.Empty;
            }
            else
            {
                ErrorMessage = result.Error ?? "Error al registrar la empresa.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }
}
