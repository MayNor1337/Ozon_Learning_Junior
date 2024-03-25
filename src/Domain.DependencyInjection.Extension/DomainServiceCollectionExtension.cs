using Domain.Models.Options;
using Domain.Separated.Repositories;
using Domain.Services;
using Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Domain.DependencyInjection.Extensions;

public static class DomainServiceCollectionExtension
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PriceCalculatorOptions>(configuration.GetSection("PriceCalculatorOptions"));
        services.AddScoped<IPriceCalculator, PriceCalculatorService>(x =>
        {
            PriceCalculatorOptions options = x.GetRequiredService<IOptionsSnapshot<PriceCalculatorOptions>>().Value;
            return new PriceCalculatorService(options, x.GetRequiredService<IStorageRepository>());
        });

        return services;
    }
}