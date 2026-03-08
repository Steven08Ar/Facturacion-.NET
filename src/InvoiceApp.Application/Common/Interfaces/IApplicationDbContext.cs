using InvoiceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Company> Companies { get; }
    DbSet<User> Users { get; } // Requires Microsoft.AspNetCore.Identity.EntityFrameworkCore in Application or using an abstraction
    DbSet<Customer> Customers { get; }
    DbSet<Product> Products { get; }
    DbSet<Quotation> Quotations { get; }
    DbSet<QuotationLine> QuotationLines { get; }
    DbSet<Invoice> Invoices { get; }
    DbSet<InvoiceLine> InvoiceLines { get; }
    DbSet<Payment> Payments { get; }
    DbSet<Tax> Taxes { get; }
    DbSet<AuditLog> AuditLogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
