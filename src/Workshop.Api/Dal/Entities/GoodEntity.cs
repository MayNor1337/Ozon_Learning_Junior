namespace Workshop.Api.Dal.Entities;
/// <summary>
/// GoodEntity
/// </summary>
public sealed record GoodEntity(
    int Id,
    string Name, 
    int Lenght,
    int Width,
    int Height,
    decimal Weight,
    int Count,
    decimal Price
    );