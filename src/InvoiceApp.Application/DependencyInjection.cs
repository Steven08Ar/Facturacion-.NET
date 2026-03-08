using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InvoiceApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());
        
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            // We'll add PipelineBehaviors here later for validation/logging
        });

        // Uncomment when FluentValidation is fully tested:
        // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
