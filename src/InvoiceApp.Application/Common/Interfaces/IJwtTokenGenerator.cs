using InvoiceApp.Domain.Entities;

namespace InvoiceApp.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user, IList<string> roles);
}
