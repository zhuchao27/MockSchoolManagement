using Microsoft.EntityFrameworkCore;
using MockSchoolManagement.Model;
using MockSchoolManagement.Model.EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.Infrastructure
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Name = "zc",
                    Major = MajorEnum.ComputerScience,
                    Email = "1313131@163.com"
                }
                );
        }
    }
}
