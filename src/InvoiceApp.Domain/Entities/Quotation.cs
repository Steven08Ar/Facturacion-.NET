using InvoiceApp.Domain.Common;
using InvoiceApp.Domain.Enums;

namespace InvoiceApp.Domain.Entities;

public class Quotation : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Guid CustomerId { get; set; }
    public string QuotationNumber { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime ValidUntilDate { get; set; }
    public QuotationStatus Status { get; set; } = QuotationStatus.Draft;
    
    public string Notes { get; set; } = string.Empty;
    public string TermsAndConditions { get; set; } = string.Empty;

    public decimal Subtotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Total { get; set; }

    public Company Company { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
    public ICollection<QuotationLine> Lines { get; set; } = new List<QuotationLine>();
}

public class QuotationLine : BaseEntity
{
    public Guid QuotationId { get; set; }
    public Guid? ProductId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal TaxRate { get; set; }
    public decimal LineTotal { get; set; }

    public Quotation Quotation { get; set; } = null!;
    public Product? Product { get; set; }
}
