using Levi9Challenge.Models;
using Levi9Challenge.Repositories.Interfaces;
using System.Globalization;

namespace Levi9Challenge.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {

        public static List<Student> _students = new List<Student>();
        private static int _IdCounter = 1;

        public Student Create(Student student)
        {
            student.Id = _IdCounter.ToString();
            ++_IdCounter;
            _students.Add(student);
            return student;
        }

        public Student? GetById(string studentId)
        {
            return _students.FirstOrDefault(s => s.Id.Equals(studentId));
        }

        public Student? GetByEmail(string email)
        {
            return _students.FirstOrDefault(s => s.Email.Equals(email));
        }

    }
}
