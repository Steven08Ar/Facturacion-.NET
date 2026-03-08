using InvoiceApp.Domain.Common;
using InvoiceApp.Domain.Enums;

namespace InvoiceApp.Domain.Entities;

public class Payment : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Guid InvoiceId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string ReceivedBy { get; set; } = string.Empty;

    public Company Company { get; set; } = null!;
    public Invoice Invoice { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
}
