namespace Workshop.Api.Responses.V3;

public record Report01Response(
    decimal MaxWeight,
    decimal MaxVolume,
    decimal MaxDistanceForHeaviestGood,
    decimal MaxDistanceForLargestGood,
    decimal WavgPrice);