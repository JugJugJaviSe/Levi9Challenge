using Xunit;
using Levi9Challenge.Models;
using System;
using System.Collections.Generic;

public class BasicCreationTests
{

    [Fact]
    public void Should_Create_Student()
    {

        var studentRepo = new FakeStudentRepo();
        var student = new Student { Id = "1", Name = "Test Student", Email = "test@student.com" };

        studentRepo.Add(student);
        var retrieved = studentRepo.GetById("1");

        Assert.NotNull(retrieved);
        Assert.Equal("Test Student", retrieved.Name);
        Assert.Equal("test@student.com", retrieved.Email);
    }

    [Fact]
    public void Should_Create_Canteen()
    {

        var canteen = new Canteen
        {
            Id = "10",
            Name = "Main Menza",
            Capacity = 50,
            Location = "Campus",
            WorkingHours = new List<WorkingHours>
            {
                new WorkingHours { Meal = "Lunch", From = "12:00", To = "14:00" }
            }
        };
        var canteenRepo = new FakeCanteenRepo();
        canteenRepo.Add(canteen);

        var retrieved = canteenRepo.GetById("10");

        Assert.NotNull(retrieved);
        Assert.Equal("Main Menza", retrieved.Name);
        Assert.Equal(50, retrieved.Capacity);
    }

    [Fact]
    public void Should_Create_Reservation()
    {

        var studentRepo = new FakeStudentRepo().Add(new Student { Id = "1", Name = "Student 1" });
        var canteen = new Canteen
        {
            Id = "2",
            Name = "Menza",
            Capacity = 10,
            WorkingHours = new List<WorkingHours> { new WorkingHours { Meal = "Lunch", From = "12:00", To = "14:00" } }
        };
        var canteenRepo = new FakeCanteenRepo();
        canteenRepo.Add(canteen);
        var reservationRepo = new FakeReservationRepo();

        var service = ReservationServiceFactory.Create(studentRepo, canteenRepo, reservationRepo);

        var result = service.Create("1", "2", "2025-12-01", "12:30", "30");

        Assert.NotNull(result);
        Assert.Equal("1", result.StudentId);
        Assert.Equal("2", result.CanteenId);
        Assert.Equal("12:30", result.Time);
        Assert.Equal(30, result.Duration);
    }
}
