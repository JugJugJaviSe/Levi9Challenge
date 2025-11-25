using Levi9Challenge.DTOs;
using Levi9Challenge.Models;
using System.Globalization;

namespace Levi9Challenge.Services.Interfaces
{
    public interface ICanteenService
    {
        Canteen Create(Canteen canteen, string studentId);
        List<Canteen> GetAll();
        Canteen GetById(string id);
        Canteen Update(CanteenDto canteen, string studentId);
        bool Delete(string canteenId, string studentId);
    }
}
