namespace Workshop.Api.Responses.V2;

public record Report01Response(
    double MaxWeight,
    double MaxVolume,
    double MaxDistanceForHeaviestGood,
    double MaxDistanceForLargestGood,
    double WavgPrice);