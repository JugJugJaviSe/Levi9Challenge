using Levi9Challenge.Models;
using Levi9Challenge.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

public class FakeReservationRepo : IReservationRepository
{
    private readonly List<Reservation> _reservations = new();
    private int _idCounter = 1;

    public FakeReservationRepo Add(Reservation r)
    {
        r.Id = (_idCounter++).ToString();
        _reservations.Add(r);
        return this;
    }

    public Reservation? Create(Reservation reservation)
    {
        reservation.Id = (_idCounter++).ToString();
        _reservations.Add(reservation);
        return reservation;
    }

    public Reservation Delete(string reservationId)
    {
        throw new NotImplementedException();
    }

    public List<Reservation> GetAll()
    {
        return _reservations.ToList();
    }

    public List<Reservation> GetAllByStudentId(string studentId)
    {
        return _reservations.Where(r => r.StudentId == studentId).ToList();
    }

    public Reservation GetById(string reservationId)
    {
        throw new NotImplementedException();
    }
}
