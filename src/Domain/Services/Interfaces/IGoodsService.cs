using Domain.Entities;

namespace Domain.Services.Interfaces;

public interface IGoodsService
{
    public IEnumerable<GoodEntity> GetGood();

}