namespace Workshop.Api.Responses.V2;

public record Report01Response(
    decimal MaxWeight,
    decimal MaxVolume,
    decimal MaxDistanceForHeaviestGood,
    decimal MaxDistanceForLargestGood,
    decimal WavgPrice);