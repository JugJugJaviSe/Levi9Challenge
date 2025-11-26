using Levi9Challenge.Repositories.Interfaces;
using Levi9Challenge.Services;
using Levi9Challenge.Services.Implementations;

public static class ReservationServiceFactory
{
    public static ReservationService Create(
        IStudentRepository students,
        ICanteenRepository canteens,
        IReservationRepository reservations)
    {
        return new ReservationService(reservations, students, canteens);
    }
}

