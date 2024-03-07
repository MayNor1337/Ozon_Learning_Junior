using System.Linq.Expressions;
using Workshop.Api.Bll.Models;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Dal.Entities;
using Workshop.Api.Dal.Repositories.Interfaces;

namespace Workshop.Api.Bll.Services;

public class PriceCalculatorService : IPriceCalculator
{
    private readonly IStorageRepository _storageRepository;
    private const double VolumeRatio = 3.27;

    public PriceCalculatorService(IStorageRepository storageRepository)
    {
        _storageRepository = storageRepository;
    }

    public double CalculatePrice(IEnumerable<GoodModels> goods)
    {
        if (goods.Any())
        {
            throw new ArgumentException("The array must not be empty");
        }

        var volume = goods
            .Sum(x => x.Height * x.Lenght * x.Width);

        double price = volume * VolumeRatio / 1000;
        
        _storageRepository.Save(new StorageEntity(
            volume,
            price,
            DateTime.UtcNow));
        
        return price;
    }

    public IEnumerable<CalculationLogModel> QueryLog(int take)
    {
        if (take <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(take), take, "Take must be greater than zero");
        }
        
        var log = _storageRepository.Query()
            .OrderBy(x => x.At)
            .Take(take);

        return log.Select(x => new CalculationLogModel(x.Volume, x.Price));
    }
}