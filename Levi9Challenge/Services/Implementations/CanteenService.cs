using Levi9Challenge.DTOs;
using Levi9Challenge.Models;
using Levi9Challenge.Repositories.Implementations;
using Levi9Challenge.Repositories.Interfaces;
using Levi9Challenge.Services.Interfaces;

namespace Levi9Challenge.Services.Implementations
{
    public class CanteenService : ICanteenService
    {
        private readonly ICanteenRepository _canteenRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly IReservationRepository _reservationRepo;

        public CanteenService(ICanteenRepository canteenRepo, IStudentRepository studentRepo, IReservationRepository reservationRepo)
        {
            _canteenRepo = canteenRepo;
            _studentRepo = studentRepo;
            _reservationRepo = reservationRepo;
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

        public List<CanteenStatusDto> GetAllCanteensStatus(
    DateTime startDate,
    DateTime endDate,
    TimeSpan startTime,
    TimeSpan endTime,
    int durationMinutes)
        {
            var result = new List<CanteenStatusDto>();
            var allCanteens = _canteenRepo.GetAll();

            foreach (var canteen in allCanteens)
            {
                var dto = GenerateStatusForCanteen(
                    canteen,
                    startDate,
                    endDate,
                    startTime,
                    endTime,
                    durationMinutes
                );

                result.Add(dto);
            }

            return result;
        }


        public CanteenStatusDto GetCanteenStatus(
    int id,
    DateTime startDate,
    DateTime endDate,
    TimeSpan startTime,
    TimeSpan endTime,
    int durationMinutes)
        {
            var canteen = _canteenRepo.GetById(id.ToString());
            if (canteen == null)
                return null;

            return GenerateStatusForCanteen(
                canteen,
                startDate,
                endDate,
                startTime,
                endTime,
                durationMinutes
            );
        }

        private CanteenStatusDto GenerateStatusForCanteen(
    Canteen canteen,
    DateTime startDate,
    DateTime endDate,
    TimeSpan startTime,
    TimeSpan endTime,
    int durationMinutes)
        {
            var dto = new CanteenStatusDto { CanteenId = canteen.Id };

            for (var date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                foreach (var wh in canteen.WorkingHours)
                {
                    var mealStart = TimeSpan.Parse(wh.From);
                    var mealEnd = TimeSpan.Parse(wh.To);

                    var slotStartTime = mealStart < startTime ? startTime : mealStart;
                    var slotEndTime = mealEnd > endTime ? endTime : mealEnd;

                    for (var slot = slotStartTime;
                         slot + TimeSpan.FromMinutes(durationMinutes) <= slotEndTime;
                         slot += TimeSpan.FromMinutes(durationMinutes))
                    {
                        var slotStartDateTime = date + slot;
                        var slotEndDateTime = slotStartDateTime + TimeSpan.FromMinutes(durationMinutes);

                        var overlappingReservations = _reservationRepo.GetAll()
                            .Where(r => r.CanteenId == canteen.Id &&
                                        r.Status == ReservationState.Active &&
                                        r.Date == date &&
                                        (r.Date + r.Time) < slotEndDateTime &&
                                        (r.Date + r.Time + TimeSpan.FromMinutes(r.Duration)) > slotStartDateTime)
                            .Count();

                        var remainingCapacity = canteen.Capacity - overlappingReservations;

                        dto.Slots.Add(new CanteenSlotDto
                        {
                            Date = date.ToString("yyyy-MM-dd"),
                            Meal = wh.Meal,
                            StartTime = slot.ToString(@"hh\:mm"),
                            RemainingCapacity = remainingCapacity
                        });
                    }
                }
            }

            return dto;
        }


    }
}
