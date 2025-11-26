using Levi9Challenge.DTOs;
using Levi9Challenge.Models;
using Levi9Challenge.Repositories.Interfaces;
using Levi9Challenge.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Levi9Challenge.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly ICanteenRepository _canteenRepo;

        public ReservationService(IReservationRepository reservationRepo, IStudentRepository studentRepo, ICanteenRepository canteenRepo) {
            _reservationRepo = reservationRepo;
            _studentRepo = studentRepo;
            _canteenRepo = canteenRepo;
        }

        public ReservationDto Create(string studentId, string canteenId, string date, string time, string duration)
        {

            var student = _studentRepo.GetById(studentId);
            if (student == null)
            {
                return new ReservationDto();
            }

            var canteen = _canteenRepo.GetById(canteenId);
            if(canteen == null)
            {
                return new ReservationDto();
            }

            if (!DateTime.TryParse(date, out var parsedDate) ||
                !TimeSpan.TryParse(time, out var parsedTime) ||
                !int.TryParse(duration, out var parsedDuration))
            {
                return new ReservationDto();

            }
            else if (parsedDate < DateTime.Today || (parsedDuration != 30 && parsedDuration != 60))
            {
                return new ReservationDto();
            }
            else if (parsedTime.Minutes != 0 && parsedTime.Minutes != 30)
            {
                return new ReservationDto();
            }
            else if (parsedDate < DateTime.Today && parsedTime < DateTime.Now.TimeOfDay)
            {
                return new ReservationDto();
            }

            var requestedStart = parsedDate + parsedTime;
            var requestedEnd = requestedStart.AddMinutes(parsedDuration);

            var overlappingReservations = _reservationRepo.GetAll()
    .Where(r => r.CanteenId == canteenId &&
                r.Status == ReservationState.Active &&
                (r.Date + r.Time) < requestedEnd &&
                (r.Date + r.Time + TimeSpan.FromMinutes(r.Duration)) > requestedStart)
    .Count();

            if (overlappingReservations >= canteen.Capacity)    //there is no more capacity for this period
            {
                return new ReservationDto();
            }

            var studentReservations = _reservationRepo.GetAllByStudentId(studentId)
                                           .Where(r => r.Status == ReservationState.Active);

            foreach (var r in studentReservations)
            {
                var existingStart = r.Date + r.Time;
                var existingEnd = existingStart.AddMinutes(r.Duration);

                bool overlaps = requestedStart < existingEnd && existingStart < requestedEnd;   //student shouldn't have more than 1 reservation at a time
                if (overlaps)
                    return new ReservationDto();
            }

            var requestedStartTime = parsedTime;
            var requestedEndTime = parsedTime.Add(TimeSpan.FromMinutes(parsedDuration));

            if (!IsInsideWorkingHours(canteen, requestedStartTime, requestedEndTime))
            {
                return new ReservationDto();
            }

            var reservation = new Reservation
                {
                    StudentId = studentId,
                    CanteenId = canteenId,
                    Date = parsedDate,
                    Time = parsedTime,
                    Duration = parsedDuration,
                    Status = ReservationState.Active
                };

            reservation = _reservationRepo.Create(reservation);

            return new ReservationDto
            {
                Id = reservation.Id,
                StudentId = reservation.StudentId,
                CanteenId = reservation.CanteenId,
                Date = reservation.Date.ToString("yyyy-MM-dd"),
                Time = reservation.Time.ToString(@"hh\:mm"),
                Duration = reservation.Duration,
                Status = reservation.Status.ToString()
            };
        }

        private bool IsInsideWorkingHours(Canteen c, TimeSpan start, TimeSpan end)
        {
            foreach (var wh in c.WorkingHours)
            {
                if (!TimeSpan.TryParse(wh.From, out var fromTime)) continue;
                if (!TimeSpan.TryParse(wh.To, out var toTime)) continue;

                if (start >= fromTime && end <= toTime)
                    return true;
            }

            return false;
        }


        public ReservationDto Delete(string reservationId, string studentId)
        {
            var student = _studentRepo.GetById(studentId);
            if (student == null)
            {
                throw new UnauthorizedAccessException();
            }

            var reservation = _reservationRepo.GetById(reservationId);

            if(reservation == null)
            {
                return new ReservationDto();

            }else if(student.Id != reservation.StudentId){

                throw new UnauthorizedAccessException();
            }

            var cancelledReservation = _reservationRepo.Delete(reservationId);

            return new ReservationDto
            {
                Id = cancelledReservation.Id,
                Status = cancelledReservation.Status.ToString(),
                StudentId = cancelledReservation.StudentId,
                CanteenId = cancelledReservation.CanteenId,
                Date = cancelledReservation.Date.ToString("yyyy-MM-dd"),
                Time = cancelledReservation.Time.ToString(@"hh\:mm"),
                Duration = cancelledReservation.Duration
            };

        }

    }
}
