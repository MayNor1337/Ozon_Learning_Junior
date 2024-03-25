using System.Net;
using Api.ActionFilters;
using Api.Controllers;
using Api.HostedServices;
using Api.Middlewares;
using Domain.DependencyInjection.Extensions;
using Domain.Models.Options;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Api;

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
        void SetupAction(MvcOptions x)
        {
            x.Filters.Add(new ExceptionFilter());
            x.Filters.Add(new ResponseTypeAttribute((int)HttpStatusCode.InternalServerError));
            x.Filters.Add(new ResponseTypeAttribute((int)HttpStatusCode.BadRequest));
            x.Filters.Add(new ProducesResponseTypeAttribute((int)HttpStatusCode.OK));
        }

        services
            .AddInfrastructure(_configuration)
            .AddDomain(_configuration)
            .AddControllers()
            .AddMvcOptions(SetupAction).Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(o =>
            {
                o.CustomSchemaIds(x => x.FullName);
            })
            .AddHostedService<GoodSyncHostedService>()
            .AddHttpContextAccessor();
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