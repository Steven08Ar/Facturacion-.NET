using MediatR;
using InvoiceApp.Domain.Common;

namespace InvoiceApp.Application.Auth.Commands.RegisterCompany;

public record RegisterCompanyCommand(
    string CompanyName,
    string TaxId,
    string AdminEmail,
    string AdminFullName,
    string Password
) : IRequest<Result<string>>;
