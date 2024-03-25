using Domain.Models.Options;
using Domain.Separated;
using Domain.Separated.Repositories;
using Domain.Services;
using Domain.Services.Interfaces;
using Infrastructure.DataAccess.Analytics;
using Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAnalyticsCollection, AnalyticsCollectionService>();
        services.AddSingleton<IGoodRepository, GoodRepository>();
        services.AddSingleton<IStorageRepository, StorageRepository>();
        services.AddScoped<IGoodsService, GoodsService>();
        services.Configure<GoodHostedServiceOptions>(configuration.GetSection("GoodHostedServiceOptions"));
        
        return services;
    }
}