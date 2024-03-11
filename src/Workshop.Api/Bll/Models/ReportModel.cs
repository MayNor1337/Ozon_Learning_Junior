namespace Workshop.Api.Bll.Models;

public record ReportModel(
    double MaxWeight,
    double MaxVolume,
    double MaxDistanceForHeaviestGood,
    double MaxDistanceForLargestGood,
    double WavgPrice);