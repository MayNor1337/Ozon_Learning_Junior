using Api.Requests.V2;
using Api.Responses.V2;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Separated;
using Domain.Services.Interfaces;

namespace Api.Controllers.V2;

[ApiController]     
[Route("v2/[controller]")]
public class DeliveryPriceController
{
    private readonly IPriceCalculator _priceCalculator;
    private readonly IAnalyticsCollection _analyticsCollection;

    public DeliveryPriceController(IPriceCalculator priceCalculator, IAnalyticsCollection analyticsCollection)
    {
        _priceCalculator = priceCalculator;
        _analyticsCollection = analyticsCollection;
    }

    [HttpPost("calculate")]
    public CalculateResponse Calculate(CalculateRequest request)
    {
        var result = _priceCalculator.CalculatePrice(
            request.Goods
                .Select(x => new GoodModels(
                    x.Lenght,
                    x.Width,
                    x.Height,
                    x.Weight)),
            1);

        return new CalculateResponse(result);
    }
    
    [HttpPost("get-history")]
    public IEnumerable<GetHistoryResponse> GetHistory(GetHistoryRequest request)
    {
        var log = _priceCalculator.QueryLog(request.Take);

        return log
            .Select(x => new GetHistoryResponse(
                new CargoResponse(x.Volume, x.Weight),
                x.Price));
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