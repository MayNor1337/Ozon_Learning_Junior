using Domain.Models;
using Domain.Models.Options;
using Domain.Separated.Repositories;
using Domain.Services.Interfaces;
using Workshop.Api.Dal.Entities;

namespace Domain.Services;

internal class PriceCalculatorService : IPriceCalculator
{
    private readonly decimal _volumeToPriceRation;
    private readonly decimal _weightToPriceRation;
    private readonly IStorageRepository _storageRepository;

    public PriceCalculatorService(
        PriceCalculatorOptions options,
        IStorageRepository storageRepository)
    {
        _volumeToPriceRation = options.VolumeToPriceRation;
        _weightToPriceRation = options.WeightToPriceRation;
        _storageRepository = storageRepository;
    } 

    public decimal CalculatePrice(IEnumerable<GoodModels> goods, decimal distance)
    {
        var goodModelsEnumerable = goods as GoodModels[] ?? goods.ToArray();
        if (goodModelsEnumerable.Any() is false)
        {
            throw new ArgumentException("The array must not be empty");
        }

        var volumePrice = Volume(goodModelsEnumerable, out var volume);

        var weightPrice = WeightPrice(goodModelsEnumerable, out var weight);

        decimal distanceInKilometers = distance / 1000m;
        decimal resultPrice = Math.Max(volumePrice, weightPrice) * distanceInKilometers;
        
        _storageRepository.Save(new StorageEntity(
            volume,
            resultPrice,
            weight,
            distance,
            goods.Count(),
            DateTime.UtcNow));
        
        return resultPrice;
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

        return log.Select(x => new CalculationLogModel(x.Volume, x.Price, x.Weight, x.Distance));
    }

    public void ClearLogs()
    {
        _storageRepository.Clear();
    }

    private decimal WeightPrice(IEnumerable<GoodModels> goods, out decimal weight)
    {
        weight = goods.Sum(x => x.Weight);

        decimal weightPrice = weight * _weightToPriceRation;
        return weightPrice;
    }

    private decimal Volume(IEnumerable<Models.GoodModels> goods, out int volume)
    {
        volume = goods
            .Sum(x => x.Height * x.Lenght * x.Width);

        decimal volumePrice = volume * _volumeToPriceRation;
        return volumePrice;
    }
}