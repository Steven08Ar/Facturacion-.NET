using InvoiceApp.Domain.Common;
using InvoiceApp.Domain.Enums;

namespace InvoiceApp.Domain.Entities;

public class Product : BaseEntity
{
    public Guid CompanyId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ProductType ProductType { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TaxRate { get; set; }
    public string Unit { get; set; } = string.Empty; // pieza, hora, kg
    public Guid? CategoryId { get; set; }
    public decimal? Stock { get; set; }
    public bool IsActive { get; set; } = true;

    public Company Company { get; set; } = null!;
}
