using InvoiceApp.Domain.Entities;
using InvoiceApp.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace InvoiceApp.Infrastructure.Persistence;

public static class DataSeeder
{
    public static async Task SeedDataAsync(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (!context.Companies.Any())
        {
            var company = new Company
            {
                Id = Guid.NewGuid(),
                Name = "Acme Corp",
                TaxId = "NIT-123456789",
                Address = "123 Business St.",
                Phone = "+1 555-0100",
                Email = "contact@acmecorp.com",
                Currency = "USD",
                TaxRegime = "General",
                InvoicePrefix = "INV"
            };
            context.Companies.Add(company);
            await context.SaveChangesAsync();

            // Roles
            var roles = new[] { "Admin", "Manager", "Billing", "ReadOnly" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // User
            var adminUser = new User
            {
                UserName = "admin@acmecorp.com",
                Email = "admin@acmecorp.com",
                CompanyId = company.Id,
                FullName = "Admin User",
                Role = "Admin",
                EmailConfirmed = true
            };

            var userResult = await userManager.CreateAsync(adminUser, "Admin123!");
            if (userResult.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Customer
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                CompanyId = company.Id,
                CustomerType = CustomerType.Business,
                Name = "Tech Solutions LLC",
                TaxId = "NIT-987654321",
                Email = "billing@techsolutions.com",
                Phone = "+1 555-0200",
                Address = "456 Tech Ave.",
                CreditLimit = 10000m,
                PaymentTermDays = 30,
                CustomerCategory = "Gold",
                IsActive = true
            };
            context.Customers.Add(customer);

            // Product
            var product = new Product
            {
                Id = Guid.NewGuid(),
                CompanyId = company.Id,
                Code = "SRV-001",
                Name = "Consultoría IT",
                Description = "Servicios de consultoría de software",
                ProductType = ProductType.Service,
                UnitPrice = 150m,
                TaxRate = 19m,
                Unit = "Hora",
                IsActive = true
            };
            context.Products.Add(product);

            // Taxes
            context.Taxes.Add(new Tax { Id = Guid.NewGuid(), CompanyId = company.Id, Name = "IVA 19%", Rate = 19m, IsDefault = true });
            
            await context.SaveChangesAsync();
        }
    }
}
