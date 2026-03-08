using Microsoft.AspNetCore.Identity;

namespace InvoiceApp.Domain.Entities;

// Note: Using Microsoft.AspNetCore.Identity here. In a strictly pure Domain, 
// we might abstract this, but it's common practice to use IdentityUser directly.

public class User : IdentityUser
{
    public Guid CompanyId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime? LastLogin { get; set; }
    public bool IsActive { get; set; } = true;
    public string? AvatarUrl { get; set; }

    public Company Company { get; set; } = null!;
}
