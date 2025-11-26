using Levi9Challenge.Models;

namespace Levi9Challenge.Repositories.Interfaces
{
    public interface IReservationRepository
    {
        Reservation Create(Reservation reservation);
        Reservation Delete(string reservationId);
        Reservation GetById(string reservationId);
        List<Reservation> GetAllByStudentId(string studentId);
        List<Reservation> GetAll();
    }
}
