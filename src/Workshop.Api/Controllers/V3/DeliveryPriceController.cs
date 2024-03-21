﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Workshop.Api.ActionFilters;
using Workshop.Api.Bll.Models;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Requests.V3;
using Workshop.Api.Responses.V3;
using Workshop.Api.Validators;

namespace Workshop.Api.Controllers.V3;

[ApiController]     
[Route("v3/[controller]")]
[ExceptionFilter]
public class DeliveryPriceController : ControllerBase
{
    private readonly IPriceCalculator _priceCalculator;
    private readonly IAnalyticsCollection _analyticsCollection;

    public DeliveryPriceController(IPriceCalculator priceCalculator, IAnalyticsCollection analyticsCollection)
    {
        _priceCalculator = priceCalculator;
        _analyticsCollection = analyticsCollection;
    }

    [HttpPost("calculate")]
    public async Task<PriceResponse> Calculate(CalculateRequest request)
    {
        var validator = new CalculateRequestValidator();
        await validator.ValidateAndThrowAsync(request);
            
        var price = _priceCalculator.CalculatePrice(
            request.Goods
                .Select(x => new GoodModels(
                    x.Lenght,
                    x.Width,
                    x.Height,
                    x.Weight)), request.Distance);

        return new PriceResponse(price);
    }
    
    [HttpPost("get-history")]
    public IEnumerable<GetHistoryResponse> GetHistory(GetHistoryRequest request)
    {
        var log = _priceCalculator.QueryLog(request.Take);

        return log
            .Select(x => new GetHistoryResponse(
                new CargoResponse(x.Volume, x.Weight),
                x.Price, x.Distance));
    }
    
    [HttpPost("delete-history")]
    public void DeleteHistory()
    {
        _priceCalculator.ClearLogs();
    }
    
    [HttpPost("reports/01")]
    public Report01Response Report01()
    {
        var report = _analyticsCollection.GetReports();
        return new Report01Response(
            report.MaxWeight,
            report.MaxVolume,
            report.MaxDistanceForHeaviestGood,
            report.MaxDistanceForLargestGood,
            report.WavgPrice);
    }
}