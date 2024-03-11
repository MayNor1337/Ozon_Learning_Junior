namespace Workshop.Api.Bll.Models;

public record CalculationLogModel(
    double Volume,
    double Price,
    double Weight,
    double Distance = 1);