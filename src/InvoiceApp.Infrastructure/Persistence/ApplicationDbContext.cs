using InvoiceApp.Application.Common.Interfaces;
using InvoiceApp.Domain.Common;
using InvoiceApp.Domain.Entities;
using InvoiceApp.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InvoiceApp.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
{
    private readonly AuditableEntityInterceptor _auditableEntityInterceptor;
    private readonly SoftDeleteInterceptor _softDeleteInterceptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        AuditableEntityInterceptor auditableEntityInterceptor,
        SoftDeleteInterceptor softDeleteInterceptor) 
        : base(options)
    {
        _auditableEntityInterceptor = auditableEntityInterceptor;
        _softDeleteInterceptor = softDeleteInterceptor;
    }

    public DbSet<Company> Companies => Set<Company>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Quotation> Quotations => Set<Quotation>();
    public DbSet<QuotationLine> QuotationLines => Set<QuotationLine>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceLine> InvoiceLines => Set<InvoiceLine>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Tax> Taxes => Set<Tax>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Apply Configurations from Assembly (if we add IEntityTypeConfiguration later)
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Global Query Filters (Soft Delete)
        builder.Entity<Company>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Customer>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Quotation>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<QuotationLine>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Invoice>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<InvoiceLine>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Payment>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Tax>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<AuditLog>().HasQueryFilter(x => !x.IsDeleted);

        // Define decimal precisions across the models to avoid warnings
        foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,2)");
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntityInterceptor, _softDeleteInterceptor);
    }
}
