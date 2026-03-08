using MediatR;
using InvoiceApp.Domain.Common;

namespace InvoiceApp.Application.Auth.Commands.Login;

public record LoginQuery(string Email, string Password) : IRequest<Result<LoginResponse>>;

public record LoginResponse(string Token, string FullName, string Role);
