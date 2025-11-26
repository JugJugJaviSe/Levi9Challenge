using Levi9Challenge.Models;
using Levi9Challenge.Repositories.Interfaces;
using Levi9Challenge.Services.Interfaces;

namespace Levi9Challenge.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        public StudentService(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

        public Student Create(Student student)
        {
            var existingStudent = _studentRepo.GetByEmail(student.Email);

            if (existingStudent != null) {
                return null;
            }

            return _studentRepo.Create(student);
        }

        public Student GetById(string id) {
            return _studentRepo.GetById(id);
        }
    }
}
