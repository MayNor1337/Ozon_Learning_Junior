using Microsoft.AspNetCore.Mvc;
using Workshop.Api.Bll.Models;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Dal.Entities;
using Workshop.Api.Dal.Repositories.Interfaces;
using Workshop.Api.Responses.V3;

namespace Workshop.Api.Controllers.V1;

[Route("goods")]
[ApiController]
public class GoodsController : Controller
{
    private IGoodRepository _repository;
    private readonly ILogger<GoodsController> _logger;

    public GoodsController(IGoodRepository repository, ILogger<GoodsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<GoodEntity> GetAll()
    {
        return _repository.GetAll();
    }

    [HttpGet("calculate/{id}")]
    public PriceResponse Calculate(
        [FromServices] IPriceCalculator priceCalculator,
        int id)
    {
        _logger.LogInformation(this.HttpContext.Request.Path);
        var good = _repository.Get(id);
        var model = new GoodModels(
            good.Lenght,
            good.Width,
            good.Height,
            good.Weight);

        var price = priceCalculator.CalculatePrice(new[] { model }, 1);

        return new PriceResponse(price);
    }
    
    [HttpGet("total_price/{id}&{distance}")]
    public PriceResponse TotalPrice(
        [FromServices] IPriceCalculator priceCalculator,
        [FromServices] IGoodRepository goodRepository,
        int id,
        int distance)
    {
        _logger.LogInformation(this.HttpContext.Request.Path);
        var good = _repository.Get(id);
        var model = new GoodModels(
            good.Lenght,
            good.Width,
            good.Height,
            good.Weight);

        var deliveryPrice = priceCalculator.CalculatePrice(new[] { model }, distance);
        var resultPrice = deliveryPrice + goodRepository.Get(id).Price;
        
        return new PriceResponse(resultPrice);
    }
}