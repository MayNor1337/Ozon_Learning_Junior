namespace Api.Responses.V2;

public record GetHistoryResponse(
    V2.CargoResponse Cargo,
    decimal Price);
    