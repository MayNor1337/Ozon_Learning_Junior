using Workshop.Api.Dal.Entities;

namespace Domain.Separated.Repositories;

public interface IStorageRepository
{
    void Save(StorageEntity entity);

    IEnumerable<StorageEntity> Query();

    void Clear();
}