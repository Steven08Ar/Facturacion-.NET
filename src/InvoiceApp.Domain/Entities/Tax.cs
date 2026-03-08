using InvoiceApp.Domain.Common;

namespace InvoiceApp.Domain.Entities;

public class Tax : BaseEntity
{
    public Guid CompanyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; } = true;

    public Company Company { get; set; } = null!;
}
