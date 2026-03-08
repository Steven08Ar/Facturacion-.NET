using InvoiceApp.Domain.Common;
using InvoiceApp.Domain.Enums;

namespace InvoiceApp.Domain.Entities;

public class Customer : BaseEntity
{
    public Guid CompanyId { get; set; }
    public CustomerType CustomerType { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty; // Value objects can be mapped to strings or owned entities
    public decimal CreditLimit { get; set; }
    public int PaymentTermDays { get; set; }
    public string CustomerCategory { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public decimal TotalDebt { get; set; } // Should ideally be calculated, but kept as property based on prompt

    public Company Company { get; set; } = null!;
    public ICollection<Quotation> Quotations { get; set; } = new List<Quotation>();
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
