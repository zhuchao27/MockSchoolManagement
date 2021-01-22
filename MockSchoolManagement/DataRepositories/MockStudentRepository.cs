using MockSchoolManagement.Infrastructure;
using MockSchoolManagement.Model;
using MockSchoolManagement.Model.EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.DataRepositories
{
    public class MockStudentRepository:IStudentRepository
    {
        private readonly AppDbContext context;

        private readonly List<Student> _studentList;

        public MockStudentRepository(AppDbContext context)
        {
            this.context = context;
            //_studentList = new List<Student>()
            //{
            //new Student() { Id = 1, Name = "张三", Major = MajorEnum.ComputerScience, Email = "zhangsan@52abp.com" },
            //new Student() { Id = 2, Name = "李四", Major = MajorEnum.ElectronicCommerce, Email = "lisi@52abp.com" },
            //new Student() { Id = 3, Name = "赵六", Major = MajorEnum.Mathematics, Email = "zhaoliu@52abp.com" },
            //};
        }

        public Student GetStudentById(int id)
        {
            return context.Students.FirstOrDefault(a => a.Id == id);
            //return _studentList.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return context.Students;
            //return _studentList;
        }

        public Student Insert(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();
            //student.Id = _studentList.Max(s => s.Id) + 1;
            //_studentList.Add(student);
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
            //Student student = _studentList.FirstOrDefault(s => s.Id == id);
            //if (student != null)
            //{
            //    _studentList.Remove(student);
            //}
            //return student;
        }

        public Student Update(Student student)
        {
            var _student = context.Students.Attach(student);
            _student.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return student;
            //Student _student = _studentList.FirstOrDefault(s => s.Id == student.Id);
            //if (_student != null)
            //{
            //    _student.Name = student.Name;
            //    _student.Email = student.Email;
            //    _student.Major = student.Major;
            //}
            //return _student;
        }
    }
}
