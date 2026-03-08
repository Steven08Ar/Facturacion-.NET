using InvoiceApp.Application.Common.Interfaces;
using InvoiceApp.Domain.Common;
using InvoiceApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InvoiceApp.Application.Auth.Commands.RegisterCompany;

public class RegisterCompanyCommandHandler : IRequestHandler<RegisterCompanyCommand, Result<string>>
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public RegisterCompanyCommandHandler(IApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<Result<string>> Handle(RegisterCompanyCommand request, CancellationToken cancellationToken)
    {
        // 1. Verificar si el usuario ya existe
        var existingUser = await _userManager.FindByEmailAsync(request.AdminEmail);
        if (existingUser != null)
        {
            return Result<string>.Failure("El correo electrónico ya está en uso.");
        }

        // 2. Crear nueva Compañía
        var company = new Company
        {
            Id = Guid.NewGuid(),
            Name = request.CompanyName,
            TaxId = request.TaxId,
            Currency = "USD",
            InvoicePrefix = "INV"
        };
        _context.Companies.Add(company);

        // 3. Crear nuevo Usuario Administrador
        var adminUser = new User
        {
            UserName = request.AdminEmail,
            Email = request.AdminEmail,
            FullName = request.AdminFullName,
            CompanyId = company.Id,
            Role = "Admin",
            IsActive = true,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(adminUser, request.Password);
        
        if (!result.Succeeded)
        {
            return Result<string>.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        await _userManager.AddToRoleAsync(adminUser, "Admin");

        // 4. Guardar Todo
        await _context.SaveChangesAsync(cancellationToken);

        return Result<string>.Success("Empresa y administrador registrados exitosamente.");
    }
}
