namespace Workshop.Api.Responses.V3;

public record GetHistoryResponse(
    V3.CargoResponse Cargo,
    double Price,
    double Distance);
    