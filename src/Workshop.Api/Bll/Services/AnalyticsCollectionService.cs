using Workshop.Api.Bll.Models;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Dal.Entities;
using Workshop.Api.Dal.Repositories.Interfaces;

namespace Workshop.Api.Bll.Services;

public class AnalyticsCollectionService : IAnalyticsCollection
{
    private readonly IStorageRepository _storage;

    public AnalyticsCollectionService(IStorageRepository storage)
    {
        _storage = storage;
    }

    public ReportModel GetReports()
    {
        var data = _storage.Query().ToArray();

        return new ReportModel(
            GetMaxWeight(data),
            GetMaxVolume(data),
            GetMaxDistanceForHeaviestGood(data),
            GetMaxDistanceForLargestGood(data),
            WavgPrice(data));
    }

    private double GetMaxWeight(IEnumerable<StorageEntity> data) => data.Max(x => x.Weight);
    
    private double GetMaxVolume(IEnumerable<StorageEntity> data) => data.Max(x => x.Volume);
    
    private double GetMaxDistanceForHeaviestGood(IEnumerable<StorageEntity> data) => data.OrderBy(x => x.Weight).First().Distance;
    
    private double GetMaxDistanceForLargestGood(IEnumerable<StorageEntity> data) => data.OrderBy(x => x.Volume).First().Distance;

    private double WavgPrice(IEnumerable<StorageEntity> data)
    {
        double weightedAverageCost = data.Sum(x => x.Price * x.Amount) / data.Sum(x => x.Amount);

        return weightedAverageCost;
    }
}