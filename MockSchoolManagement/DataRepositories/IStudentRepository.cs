using MockSchoolManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.DataRepositories
{
    public interface IStudentRepository
    {
        Student GetStudentById(int id);

        IEnumerable<Student> GetAllStudents();

        Student Insert(Student student);

        Student Update(Student student);

        Student Delete(int id);
    }
}
