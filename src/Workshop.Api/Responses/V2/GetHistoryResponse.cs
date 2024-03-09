namespace Workshop.Api.Responses.V2;

public record GetHistoryResponse(
    V2.CargoResponse Cargo,
    double Price);
    