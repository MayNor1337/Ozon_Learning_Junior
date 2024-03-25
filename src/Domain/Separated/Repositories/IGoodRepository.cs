using Domain.Entities;

namespace Domain.Separated.Repositories;

public interface IGoodRepository
{
    public void AddOrUpdate(GoodEntity entity);

    IEnumerable<GoodEntity> GetAll();
    
    GoodEntity Get(int id);
}