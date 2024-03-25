using Domain.Separated.Repositories;
using Workshop.Api.Dal.Entities;

namespace Infrastructure.DataAccess.Repositories;

public class StorageRepository : IStorageRepository
{
    private readonly List<StorageEntity> _storage = new();

    public void Save(StorageEntity entity)
    {
        _storage.Add(entity);
    }

    public IEnumerable<StorageEntity> Query()
    {
        return _storage;
    }

    public void Clear()
    {
        _storage.Clear();
    }
}