﻿using Microsoft.Extensions.Options;
using Workshop.Api.Bll.Models.Options;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Dal.Repositories.Interfaces;

namespace Workshop.Api.HostedServices;

public class GoodSyncHostedService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptionsMonitor<GoodHostedServiceOptions> _options;
    private readonly IGoodRepository _goodRepository;

    public GoodSyncHostedService(
        IOptionsMonitor<GoodHostedServiceOptions> options, 
        IGoodRepository goodRepository, 
        IServiceProvider serviceProvider)
    {
        _options = options;
        _goodRepository = goodRepository;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested == false)
        {
            double updateTime = _options.CurrentValue.UpdateTime;

            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IGoodsService>();

                var goods = service.GetGood();
                foreach (var good in goods)
                    _goodRepository.AddOrUpdate(good);
            }

            await Task.Delay(TimeSpan.FromSeconds(updateTime), stoppingToken);
        }
    }
}