using Levi9Challenge.Models;

namespace Levi9Challenge.Services.Interfaces
{
    public interface IStudentService
    {
        Student Create(Student student);
        Student GetById(string id);
    }
}
