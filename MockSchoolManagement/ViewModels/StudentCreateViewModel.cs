using Microsoft.AspNetCore.Http;
using MockSchoolManagement.Model.EnumTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.ViewModels
{
    public class StudentCreateViewModel
    {

        [Display(Name = "名字")]
        [Required(ErrorMessage = "请输入名字！")]
        public string Name { get; set; }

        [Display(Name = "主修科目")]
        [Required(ErrorMessage = "请输入科目！")]
        public MajorEnum? Major { get; set; }

        [Display(Name = "电子邮箱")]
        [RegularExpression(@"^[a-zA-Z0-9]+([-_.][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([-_.][a-zA-Z0-9]+)*\.[a-z]{2,}$", ErrorMessage = "邮箱格式不正确")]
        [Required(ErrorMessage = "请输入邮箱地址！")]
        public string Email { get; set; }

        [Display(Name="头像")]
        public List<IFormFile> Photos { get; set; }
    }
}
