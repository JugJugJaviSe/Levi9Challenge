using Levi9Challenge.Models;

namespace Levi9Challenge.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Student Create(Student student);
        Student? GetById(string studentId);
        Student? GetByEmail(string email);
    }
}
