namespace Workshop.Api.Responses.V3;

public record Report01Response(
    double MaxWeight,
    double MaxVolume,
    double MaxDistanceForHeaviestGood,
    double MaxDistanceForLargestGood,
    double WavgPrice);