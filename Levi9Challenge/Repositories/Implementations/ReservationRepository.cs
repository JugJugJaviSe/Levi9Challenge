using Levi9Challenge.Models;
using Levi9Challenge.Repositories.Interfaces;

namespace Levi9Challenge.Repositories.Implementations
{
    public class ReservationRepository : IReservationRepository
    {
        public static List<Reservation> _reservations = new List<Reservation>();
        private static int _IdCounter = 1;

        public Reservation Create(Reservation reservation)
        {
            reservation.Id = _IdCounter
                .ToString();
            ++_IdCounter;
            _reservations.Add(reservation);
            return reservation;
        }

        public Reservation Delete(string reservationId)
        {
            var reservation = GetById(reservationId);
            reservation.Status = ReservationState.Cancelled;
            return reservation;
        }

        public Reservation GetById(string reservationId)
        {
            return _reservations.FirstOrDefault(r => r.Id.ToString().Equals(reservationId));
        }

        public List<Reservation> GetAllByStudentId(string studentId)
        {
            return _reservations
                .Where(r => r.StudentId == studentId)
                .ToList();
        }

        public List<Reservation> GetAll()
        {
            return _reservations;
        }
    }
}
