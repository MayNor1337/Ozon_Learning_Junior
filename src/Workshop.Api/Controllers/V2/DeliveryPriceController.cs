using Microsoft.AspNetCore.Mvc;
using Workshop.Api.Bll.Models;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Requests.V2;
using Workshop.Api.Responses.V2;

namespace Workshop.Api.Controllers.V2;

[ApiController]     
[Route("v2/[controller]")]
public class DeliveryPriceController
{
    private readonly IPriceCalculator _priceCalculator;

    public DeliveryPriceController(IPriceCalculator priceCalculator)
    {
        _priceCalculator = priceCalculator;
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
                    x.Weight)));

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
}