using InvoiceApp.Domain.Common;
using InvoiceApp.Domain.Enums;

namespace InvoiceApp.Domain.Entities;

public class Invoice : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid? QuotationId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; }
    public DateTime DueDate { get; set; }
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;
    
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentTerms { get; set; } = string.Empty;

    public decimal Subtotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Total { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal BalanceDue { get; set; }

    public string Notes { get; set; } = string.Empty;
    public string InternalNotes { get; set; } = string.Empty;
    public string PdfUrl { get; set; } = string.Empty;

    public DateTime? SentAt { get; set; }
    public DateTime? PaidAt { get; set; }

    public Company Company { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
    public Quotation? Quotation { get; set; }
    public ICollection<InvoiceLine> Lines { get; set; } = new List<InvoiceLine>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}

public class InvoiceLine : BaseEntity
{
    public Guid InvoiceId { get; set; }
    public Guid? ProductId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal TaxRate { get; set; }
    public decimal LineTotal { get; set; }

    public Invoice Invoice { get; set; } = null!;
    public Product? Product { get; set; }
}
