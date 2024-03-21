﻿using Workshop.Api.Dal.Entities;
using Workshop.Api.Dal.Repositories.Interfaces;

namespace Workshop.Api.Dal.Repositories;

public class GoodRepository : IGoodRepository
{
    private readonly Dictionary<int, GoodEntity> _store = new Dictionary<int, GoodEntity>();

    public void AddOrUpdate(GoodEntity entity)
    {
        if (_store.ContainsKey(entity.Id))
            _store.Remove(entity.Id);
        
        _store.Add(entity.Id, entity);
    }

    public IEnumerable<GoodEntity> GetAll() 
        => _store.Select(x => x.Value);

    public GoodEntity Get(int id) 
        => _store[id];
}