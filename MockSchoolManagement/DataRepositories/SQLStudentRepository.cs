using MockSchoolManagement.Infrastructure;
using MockSchoolManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.DataRepositories
{
    public class SQLStudentRepository:IStudentRepository
    {
        private readonly AppDbContext context;

        private readonly List<Student> _studentList;

        public SQLStudentRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Student GetStudentById(int id)
        {
            return context.Students.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return context.Students;
        }

        public Student Insert(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();
            return student;
        }

        public Student Delete(int id)
        {
            Student student = context.Students.Find(id);
            if (student != null)
            {
                context.Students.Remove(student);
                context.SaveChanges();
            }
            return student;
        }

        public Student Update(Student student)
        {
            var _student = context.Students.Attach(student);
            _student.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return student;
        }
    }
}
