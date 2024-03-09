using Workshop.Api.Dal.Entities;
using Workshop.Api.Dal.Repositories.Interfaces;

namespace Workshop.Api.Dal.Repositories;

public class StorageRepository : IStorageRepository
{
    private readonly List<StorageEntity> _storage = new();
    public void Save(StorageEntity entity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<StorageEntity> Query()
    {
        throw new NotImplementedException();
    }
}