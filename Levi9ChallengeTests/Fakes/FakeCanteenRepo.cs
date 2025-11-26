using Levi9Challenge.Models;
using Levi9Challenge.Repositories.Interfaces;
using System.Collections.Generic;

public class FakeCanteenRepo : ICanteenRepository
{
    private readonly Dictionary<string, Canteen> _canteens = new();

    public FakeCanteenRepo Add(Canteen c)
    {
        _canteens[c.Id] = c;
        return this;
    }

    public Canteen Create(Canteen canteen)
    {
        throw new NotImplementedException();
    }

    public bool Delete(string id)
    {
        throw new NotImplementedException();
    }

    public List<Canteen> GetAll()
    {
        throw new NotImplementedException();
    }

    public Canteen? GetById(string id)
    {
        return _canteens.ContainsKey(id) ? _canteens[id] : null;
    }

    public Canteen Update(Canteen canteen)
    {
        throw new NotImplementedException();
    }
}
