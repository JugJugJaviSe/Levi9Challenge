using Levi9Challenge.DTOs;
using Levi9Challenge.Models;

namespace Levi9Challenge.Services.Interfaces
{
    public interface IReservationService
    {
        ReservationDto Create(string studentId, string canteenId, string date, string time, string duration);
        ReservationDto Delete(string reservationId, string studentId);
    }
}
