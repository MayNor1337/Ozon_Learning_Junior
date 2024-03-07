using Microsoft.AspNetCore.Mvc;
using Workshop.Api.Bll.Models;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Requests.V1;
using Workshop.Api.Responses.V1;

namespace Workshop.Api.Controllers.V1;

[ApiController]     
[Route("v1/[controller]")]
public class DeliveryPriceController : ControllerBase
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
                    x.Height)));

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
}
