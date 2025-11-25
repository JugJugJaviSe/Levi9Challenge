using Levi9Challenge.DTOs;
using Levi9Challenge.Models;

namespace Levi9Challenge.Repositories.Interfaces
{
    public interface ICanteenRepository
    {
        Canteen Create(Canteen canteen);
        List<Canteen> GetAll();
        Canteen? GetById(string id);
        Canteen Update(Canteen canteen);
        bool Delete(string id);
    }
}
