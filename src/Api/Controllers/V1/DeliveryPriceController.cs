using Api.Requests.V1;
using Api.Responses.V1;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Separated;
using Domain.Services.Interfaces;

namespace Api.Controllers.V1;

[ApiController]     
[Route("v1/[controller]")]
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
    public CalculateResponse Calculate(CalculateRequest request)
    {
        var result = _priceCalculator.CalculatePrice(
            request.Goods
                .Select(x => new GoodModels(
                    x.Lenght,
                    x.Width,
                    x.Height,
                    0)),
            1);

        return new CalculateResponse(result);
    }
    
    [HttpPost("get-history")]
    public IEnumerable<GetHistoryResponse> GetHistory(GetHistoryRequest request)
    {
        var log = _priceCalculator.QueryLog(request.Take);

        return log
            .Select(x => new GetHistoryResponse(
                new CargoResponse(x.Volume),
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
