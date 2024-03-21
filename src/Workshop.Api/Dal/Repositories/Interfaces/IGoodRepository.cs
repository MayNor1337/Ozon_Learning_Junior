using Workshop.Api.Dal.Entities;

namespace Workshop.Api.Dal.Repositories.Interfaces;

public interface IGoodRepository
{
    public void AddOrUpdate(GoodEntity entity);

    IEnumerable<GoodEntity> GetAll();
    
    GoodEntity Get(int id);
}