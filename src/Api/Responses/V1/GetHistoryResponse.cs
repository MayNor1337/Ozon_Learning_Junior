namespace Api.Responses.V1;

public record GetHistoryResponse(
    CargoResponse Cargo,
    decimal Price);
    