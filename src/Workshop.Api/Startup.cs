using System.Net;
using Microsoft.AspNetCore.Mvc;
using Workshop.Api.ActionFilters;
using Workshop.Api.Bll.Models.Options;
using Workshop.Api.Bll.Services;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Controllers;
using Workshop.Api.Dal.Repositories;
using Workshop.Api.Dal.Repositories.Interfaces;
using Workshop.Api.HostedServices;
using Workshop.Api.Middlewares;

namespace Workshop.Api;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment, IWebHostEnvironment webHostEnvironment)
    {
        _configuration = configuration;
        _hostEnvironment = hostEnvironment;
        _webHostEnvironment = webHostEnvironment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc()
            .AddMvcOptions(x =>
            {
                x.Filters.Add(new ExceptionFilter());
                x.Filters.Add(new ResponseTypeAttribute((int)HttpStatusCode.InternalServerError));
                x.Filters.Add(new ResponseTypeAttribute((int)HttpStatusCode.BadRequest));
                x.Filters.Add(new ProducesResponseTypeAttribute((int)HttpStatusCode.OK));
            });
        
        services.AddControllersWithViews().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
        });

        services.Configure<PriceCalculatorOptions>(_configuration.GetSection("PriceCalculatorOptions"));
        services.Configure<GoodHostedServiceOptions>(_configuration.GetSection("GoodHostedServiceOptions"));

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(o =>
        {
            o.CustomSchemaIds(x => x.FullName);
        });

        services.AddScoped<IPriceCalculator, PriceCalculatorService>();

        services.AddScoped<IAnalyticsCollection, AnalyticsCollectionService>();
        services.AddHostedService<GoodSyncHostedService>();
        
        services.AddSingleton<IGoodRepository, GoodRepository>();
        services.AddSingleton<IStorageRepository, StorageRepository>();
        services.AddScoped<IGoodsService, GoodsService>();
        services.AddHttpContextAccessor();
    }

    public void Configure(
        IHostEnvironment environment,
        IApplicationBuilder app)
    {
        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.Map("/v3/DeliveryPriceController/total_price", middleware =>
        {
            app.UseMiddleware<TotalGoodPriceLoggerMiddleware>();
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapDefaultControllerRoute();
        });
    }
}