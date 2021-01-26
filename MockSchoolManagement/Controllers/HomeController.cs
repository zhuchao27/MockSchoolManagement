using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockSchoolManagement.DataRepositories;
using MockSchoolManagement.Model;
using MockSchoolManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.Controllers
{

    public class HomeController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(IStudentRepository studentRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.studentRepository = studentRepository;
            this.webHostEnvironment = webHostEnvironment;
        }


        public ViewResult Index()
        {
            throw new Exception("测试异常");
            var model = studentRepository.GetAllStudents();
            return View(model);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentCreateViewModel model)
        {
            var context = this.HttpContext;
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (model.Photos != null && model.Photos.Count > 0)
                {
                    foreach(IFormFile photo in model.Photos)
                    {
                        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images","avatars");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                }

                Student newStudent = new Student
                {
                    Name = model.Name,
                    Email = model.Email,
                    Major = model.Major,
                    PhotoPath = uniqueFileName
                };
                 studentRepository.Insert(newStudent);
                return RedirectToAction("Details", new { id = newStudent.Id });
            }

            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Student student = studentRepository.GetStudentById(id);
            if (student == null)
            {
                ViewBag.ErrorMessage = $"学生Id={id}的信息不存在，请重试。";
                return View("NotFound");
            }
            StudentEditViewModel studentEditViewModel = new StudentEditViewModel
            {
                Id = id,
                Name = student.Name,
                Email = student.Email,
                Major = student.Major,
                ExistingPhotoPath = student.PhotoPath
            };
            return View(studentEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model)
        {

            if (ModelState.IsValid)
            {
                Student student = studentRepository.GetStudentById(model.Id);

                student.Name = model.Name;
                student.Email = model.Email;
                student.Major = model.Major;

                if (model.Photos.Count > 0)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", "avatars", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    //student.PhotoPath=

                }
                Student updateStudent = studentRepository.Update(student);
                return RedirectToAction("index");
            }
            return View(model);
            //Student student = studentRepository.GetStudentById(id);
            //StudentEditViewModel studentEditViewModel = new StudentEditViewModel
            //{
            //    Id = student.Id,
            //    Name = student.Name,
            //    Email = student.Email,
            //    Major = student.Major,
            //    ExistingPhotoPath = student.PhotoPath
            //};
            //return View(studentEditViewModel);
        }


        public ViewResult Details(int id)
        {
            Student model = studentRepository.GetStudentById(id);

            if (model == null)
            {
                Response.StatusCode = 404;
                return View("StudentNotFound", id);
            }
            //ViewData["PageTitle"] = "Student Details";
            //ViewData["Student"] = model;

            //ViewBag.PageTitle = "学生详情";
            //ViewBag.Student = model;

            HomeDetailsViewModel vm = new HomeDetailsViewModel() { Student=model,PageTitle= "学生详情" };

            return View(vm);
        }

        private string ProcessUploadedFile(StudentCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photos.Count > 0)
            {
                foreach(var photo in model.Photos)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images", "avatars");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream=new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }
            return uniqueFileName;
        }


    }
}
