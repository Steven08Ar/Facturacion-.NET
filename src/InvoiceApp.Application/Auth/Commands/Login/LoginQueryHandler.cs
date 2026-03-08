using InvoiceApp.Application.Common.Interfaces;
using InvoiceApp.Domain.Common;
using InvoiceApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InvoiceApp.Application.Auth.Commands.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<LoginResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Result<LoginResponse>.Failure("Credenciales inválidas.");
        }

        if (!user.IsActive)
        {
            return Result<LoginResponse>.Failure("La cuenta está inactiva.");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenGenerator.GenerateToken(user, roles);

        return Result<LoginResponse>.Success(new LoginResponse(token, user.FullName, roles.FirstOrDefault() ?? ""));
    }
}
