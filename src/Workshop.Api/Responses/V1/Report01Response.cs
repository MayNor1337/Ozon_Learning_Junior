namespace Workshop.Api.Responses.V1;

public record Report01Response(
    double MaxWeight,
    double MaxVolume,
    double MaxDistanceForHeaviestGood,
    double MaxDistanceForLargestGood,
    double WavgPrice);