namespace Domain.Models;

public record CalculationLogModel(
    decimal Volume,
    decimal Price,
    decimal Weight,
    decimal Distance);