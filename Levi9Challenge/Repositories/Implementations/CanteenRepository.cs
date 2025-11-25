using Levi9Challenge.DTOs;
using Levi9Challenge.Models;
using Levi9Challenge.Repositories.Interfaces;

namespace Levi9Challenge.Repositories.Implementations
{
    public class CanteenRepository : ICanteenRepository
    {

        public static List<Canteen> _canteens = new List<Canteen>();
        private static int _IdCounter = 1;

        public Canteen Create(Canteen canteen)
        {
            canteen.Id = _IdCounter.ToString();
            ++_IdCounter;
            _canteens.Add(canteen);
            return canteen;
        }

        public List<Canteen> GetAll()
        {
            return _canteens;
        }

        public Canteen? GetById(string id)
        {
            return _canteens.FirstOrDefault(c => c.Id == id);
        }

        public Canteen Update(Canteen canteen)
        {
            var existing = _canteens.FirstOrDefault(c => c.Id == canteen.Id);
            if (existing == null)
                return new Canteen();

            existing.Name = canteen.Name;
            existing.Location = canteen.Location;
            existing.Capacity = canteen.Capacity;
            existing.WorkingHours = canteen.WorkingHours;

            return existing;
        }

        public bool Delete(string id)
        {
            var canteen = _canteens.FirstOrDefault(c => c.Id.ToString().Equals(id));
            return _canteens.Remove(canteen);
        }

            
    }
}
