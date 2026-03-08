using InvoiceApp.Domain.Common;

namespace InvoiceApp.Domain.Entities;

public class Company : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[]? Logo { get; set; }
    public string Currency { get; set; } = "USD";
    public string TaxRegime { get; set; } = string.Empty;
    public string InvoicePrefix { get; set; } = "INV";

    // Navigations
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Customer> Customers { get; set; } = new List<Customer>();
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Quotation> Quotations { get; set; } = new List<Quotation>();
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public ICollection<Tax> Taxes { get; set; } = new List<Tax>();
}
