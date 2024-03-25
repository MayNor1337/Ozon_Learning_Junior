using Domain.Entities;
using Domain.Services.Interfaces;

namespace Domain.Services;

public sealed class GoodsService : IGoodsService
{
    private readonly List<GoodModels> goods = new()
    {
        new ("Парик для питомца", 1, 1000, 2000, 3000, 4000, 100),
        new ("Накидка на телевизор", 2,  1000, 2000, 3000, 4000, 120), 
        new ("Ковёр настенный", 3, 2000, 3000, 3000, 5000, 140),
        new ("Здоровенный ЯЗЬ", 4, 1000,  1000, 4000, 4000, 160),
        new ("Билет МММ", 5, 3000, 2000, 1000, 5000, 180)
    };

    public IEnumerable<GoodEntity> GetGood()
    {
        var random = new Random();
        foreach (var good in goods)
        {
            var count = random.Next(0, 10);
            yield return new GoodEntity(
                good.Id,
                good.Name,
                good.Lenght,
                good.Width,
                good.Height,
                good.Weight,
                count,
                good.Price);
        }
    }
    
    private record GoodModels(
        string Name,
        int Id,
        int Height,
        int Lenght,
        int Width,
        int Weight,
        decimal Price);
}
