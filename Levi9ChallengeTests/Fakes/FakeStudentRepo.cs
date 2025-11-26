using Levi9Challenge.Models;
using Levi9Challenge.Repositories.Interfaces;
using System.Collections.Generic;

public class FakeStudentRepo : IStudentRepository
{
    private readonly Dictionary<string, Student> _students = new();

    public FakeStudentRepo Add(Student s)
    {
        _students[s.Id] = s;
        return this;
    }

    public Student Create(Student student)
    {
        throw new NotImplementedException();
    }

    public Student? GetByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Student? GetById(string id)
    {
        return _students.ContainsKey(id) ? _students[id] : null;
    }
}
