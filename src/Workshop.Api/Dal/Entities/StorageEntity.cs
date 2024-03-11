namespace Workshop.Api.Dal.Entities;

public record StorageEntity(
    double Volume,
    double Price,
    double Weight,
    double Distance,
    int Amount,
    DateTime At);