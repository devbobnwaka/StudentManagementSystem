using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Dto;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly string _active = "active";
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository) 
        {
            this._studentRepository = studentRepository;
        }
        public async Task<IActionResult> StudentList()
        {
            ViewData["student"] = _active;
            IEnumerable<Student>? model = await _studentRepository.GetAllStudent();
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateStudent() {
            ViewData["student"] = _active;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentViewModel model)
        {
            ViewData["student"] = _active;
            if (ModelState.IsValid)
            {
                Student student = new() { 
                    Name = model.Name,
                    Class = model.Class,
                    ContactInformation = model.ContactInformation,
                    DateOfBirth = model.DateOfBirth,
                };
                await _studentRepository.CreateStudent(student);
                return RedirectToAction("GetStudent", "Student", new {id=student.Id});
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditStudent(Guid? id)
        {
            ViewData["student"] = _active;
            if (id == null)
            {
                return RedirectToAction("StudentList", "Student");
            }
            Student student = await _studentRepository.GetStudentById(id);
            if (student == null)
            {
                Response.StatusCode = 404;
                return View("StudentNotFound", id);
            }
            EditStudentViewModel studentDetailsViewModel = new()
            {
                Id = student.Id,
                Name = student.Name,
                Class = student.Class,
                ContactInformation = student.ContactInformation,
                DateOfBirth = student.DateOfBirth,
            };
            return View(studentDetailsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(EditStudentViewModel model)
        {
            ViewData["student"] = _active;
            if (ModelState.IsValid)
            {
                Student student = await _studentRepository.GetStudentById(model.Id);
                student.Name = model.Name;
                student.Class = model.Class;
                student.ContactInformation = model.ContactInformation;
                student.DateOfBirth = model.DateOfBirth;

                await _studentRepository.UpdateStudent(student);
                return RedirectToAction("GetStudent", "Student", new { id = student.Id });
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetStudent(Guid? id)
        {
            ViewData["student"] = _active;
            if (id == null)
            {
                return RedirectToAction("StudentList", "Student");
            }
            if (ModelState.IsValid) 
            {
                Student student = await _studentRepository.GetStudentById(id);
                if (student == null) 
                {
                    Response.StatusCode = 404;
                    return View("StudentNotFound", id);
                }
                CreateStudentViewModel studentDetailsViewModel = new()
                {
                    Name = student.Name,    
                    Class = student.Class,
                    ContactInformation = student.ContactInformation,
                    DateOfBirth = student.DateOfBirth,
                };
                return View(studentDetailsViewModel);
            }
            return RedirectToAction("StudentList", "Student");
        }
    }
}
