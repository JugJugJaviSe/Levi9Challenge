using Levi9Challenge.DTOs;
using Levi9Challenge.Models;
using Levi9Challenge.Repositories.Interfaces;
using Levi9Challenge.Services.Interfaces;

namespace Levi9Challenge.Services.Implementations
{
    public class CanteenService : ICanteenService
    {
        private readonly ICanteenRepository _canteenRepo;
        private readonly IStudentRepository _studentRepo;

        public CanteenService(ICanteenRepository canteenRepo, IStudentRepository studentRepo)
        {
            _canteenRepo = canteenRepo;
            _studentRepo = studentRepo;
        }

        public Canteen Create(Canteen canteen, string studentId)
        {
            var student = _studentRepo.GetById(studentId);
            if (student == null || !student.IsAdmin)
            {
                return new Canteen();
            }

            var created = _canteenRepo.Create(canteen);
            return created;
        }

        public List<Canteen> GetAll()
        {
            return _canteenRepo.GetAll();
        }

        public Canteen GetById(string id)
        {
            var canteen = _canteenRepo.GetById(id);

            if (canteen == null) {
                return new Canteen();
            }

            return canteen;
        }

        public Canteen Update(CanteenDto dto, string studentId)
        {
            var student = _studentRepo.GetById(studentId);
            if (student == null || !student.IsAdmin)
            {
                throw new UnauthorizedAccessException();
            }

            var existing = _canteenRepo.GetById(dto.Id);
            if (existing == null)
            {
                return new Canteen();
            }

            if (dto.Name != null) existing.Name = dto.Name;
            if (dto.Location != null) existing.Location = dto.Location;
            if (dto.Capacity.HasValue) existing.Capacity = dto.Capacity.Value;
            if (dto.WorkingHours != null)
            {
                existing.WorkingHours = dto.WorkingHours
                    .Select(wh => new WorkingHours
                    {
                        Meal = wh.Meal,
                        From = wh.From,
                        To = wh.To
                    })
                    .ToList();
            }

            return _canteenRepo.Update(existing);
        }

        public bool Delete(string canteenId, string studentId)
        {
            var student = _studentRepo.GetById(studentId);
            if (student == null || !student.IsAdmin)
            {
                throw new UnauthorizedAccessException();
            }

            return _canteenRepo.Delete(canteenId);

        }

    }
}
