using Workshop.Api.Bll.Models;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Dal.Entities;
using Workshop.Api.Dal.Repositories.Interfaces;

namespace Workshop.Api.Bll.Services;

public class PriceCalculatorService : IPriceCalculator
{
    private readonly IStorageRepository _storageRepository;
    private const double VolumeRatio = 3.27;
    private const double WeightRatio = 1.34;

    public PriceCalculatorService(IStorageRepository storageRepository)
    {
        _storageRepository = storageRepository;
    }

    public double CalculatePrice(IEnumerable<GoodModels> goods, double distance)
    {
        var goodModelsEnumerable = goods as GoodModels[] ?? goods.ToArray();
        if (goodModelsEnumerable.Any() is false)
        {
            throw new ArgumentException("The array must not be empty");
        }

        var volumePrice = Volume(goodModelsEnumerable, out var volume);

        var weightPrice = WeightPrice(goodModelsEnumerable, out var weight);

        double distanceInKilometers = distance / 1000d;
        double resultPrice = Math.Max(volumePrice, weightPrice) * distanceInKilometers;
        
        _storageRepository.Save(new StorageEntity(
            volume,
            resultPrice,
            weight,
            distance,
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

    private double WeightPrice(IEnumerable<GoodModels> goods, out double weight)
    {
        weight = goods.Sum(x => x.Weight);

        double weightPrice = weight * WeightRatio;
        return weightPrice;
    }

    private double Volume(IEnumerable<GoodModels> goods, out int volume)
    {
        volume = goods
            .Sum(x => x.Height * x.Lenght * x.Width);

        double volumePrice = volume * VolumeRatio;
        return volumePrice;
    }
}