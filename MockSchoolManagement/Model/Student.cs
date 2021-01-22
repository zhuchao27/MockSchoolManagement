using MockSchoolManagement.Model.EnumTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.Model
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public MajorEnum? Major { get; set; }
        public string Email { get; set; }

        public string PhotoPath { get; set; }
    }
}
