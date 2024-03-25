namespace Workshop.Api.Dal.Entities;

public record StorageEntity(
    decimal Volume,
    decimal Price,
    decimal Weight,
    decimal Distance,
    int Amount,
    DateTime At);