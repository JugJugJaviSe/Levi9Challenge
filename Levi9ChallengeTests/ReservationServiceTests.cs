using Xunit;
using Levi9Challenge.Models;
using System;
using System.Collections.Generic;

public class ReservationServiceTests
{

    [Fact]
    public void Should_Fail_When_Outside_WorkingHours()
    {
        var studentRepo = new FakeStudentRepo().Add(new Student { Id = "1", Name = "Test Student" });

        var canteen = new Canteen
        {
            Id = "2",
            Name = "Menza",
            Capacity = 10,
            WorkingHours = new List<WorkingHours>
            {
                new WorkingHours { Meal = "Dinner", From = "17:00", To = "19:00" }
            }
        };

        var canteenRepo = new FakeCanteenRepo().Add(canteen);
        var reservationRepo = new FakeReservationRepo();

        var service = ReservationServiceFactory.Create(studentRepo, canteenRepo, reservationRepo);

        var result = service.Create("1", "2", "2025-12-01", "19:30", "60");

        Assert.True(string.IsNullOrEmpty(result?.Id));
    }

    [Fact]
    public void Should_Succeed_When_Inside_WorkingHours()
    {
        var studentRepo = new FakeStudentRepo().Add(new Student { Id = "1" });

        var canteen = new Canteen
        {
            Id = "2",
            Name = "Menza",
            Capacity = 10,
            WorkingHours = new List<WorkingHours>
            {
                new WorkingHours { Meal = "Lunch", From = "12:00", To = "15:00" }
            }
        };

        var canteenRepo = new FakeCanteenRepo().Add(canteen);
        var reservationRepo = new FakeReservationRepo();

        var service = ReservationServiceFactory.Create(studentRepo, canteenRepo, reservationRepo);

        var result = service.Create("1", "2", "2025-12-01", "12:30", "30");

        Assert.NotNull(result);
    }

    [Fact]
    public void Should_Fail_When_Student_Has_Overlapping_Reservation()
    {
        var studentRepo = new FakeStudentRepo().Add(new Student { Id = "1" });

        var canteen = new Canteen
        {
            Id = "2",
            Name = "Menza",
            Capacity = 10,
            WorkingHours = new List<WorkingHours>
            {
                new WorkingHours { Meal = "Lunch", From = "12:00", To = "17:00" }
            }
        };

        var canteenRepo = new FakeCanteenRepo().Add(canteen);

        var reservationRepo = new FakeReservationRepo()
            .Add(new Reservation
            {
                StudentId = "1",
                CanteenId = "2",
                Date = new DateTime(2025, 12, 01),
                Time = TimeSpan.FromHours(13),
                Duration = 60,
                Status = ReservationState.Active
            });

        var service = ReservationServiceFactory.Create(studentRepo, canteenRepo, reservationRepo);

        var result = service.Create("1", "2", "2025-12-01", "13:30", "60");

        Assert.True(string.IsNullOrEmpty(result?.Id));
        Assert.Equal("", result?.Id);

    }

    [Fact]
    public void Should_Fail_When_Capacity_Exceeded()
    {
        var studentRepo = new FakeStudentRepo().Add(new Student { Id = "1" });

        var canteen = new Canteen
        {
            Id = "2",
            Name = "Menza",
            Capacity = 1,
            WorkingHours = new List<WorkingHours>
            {
                new WorkingHours { Meal = "Lunch", From = "12:00", To = "17:00" }
            }
        };

        var canteenRepo = new FakeCanteenRepo().Add(canteen);

        var reservationRepo = new FakeReservationRepo()
            .Add(new Reservation
            {
                StudentId = "9",
                CanteenId = "2",
                Date = new DateTime(2025, 12, 01),
                Time = TimeSpan.FromHours(14),
                Duration = 60,
                Status = ReservationState.Active
            });

        var service = ReservationServiceFactory.Create(studentRepo, canteenRepo, reservationRepo);

        var result = service.Create("1", "2", "2025-12-01", "14:00", "60");

        Assert.True(string.IsNullOrEmpty(result?.Id));
        Assert.Equal("", result?.Id);
    }

    [Fact]
    public void Should_Fail_When_Invalid_Input()
    {
        var studentRepo = new FakeStudentRepo();
        var canteenRepo = new FakeCanteenRepo().Add(new Canteen { Id = "2", Capacity = 10 });
        var reservationRepo = new FakeReservationRepo();

        var service = ReservationServiceFactory.Create(studentRepo, canteenRepo, reservationRepo);

        var result = service.Create("1", "2", "INVALID_DATE", "13:00", "60");

        Assert.True(string.IsNullOrEmpty(result?.Id));
        Assert.Equal("", result?.Id);
    }
}
