using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Dto;
using StudentManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Controllers
{
    public class ResultController : Controller
    {
        private readonly string _active = "active";
        private readonly IResultRepository _resultRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;

        public ResultController(IResultRepository resultRepository, IStudentRepository studentRepository, ISubjectRepository subjectRepository)
        {
            _resultRepository = resultRepository;
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
        }
        [HttpGet]
        public async Task<IActionResult> AddResult()
        {
            ViewData["result"] = _active;
            var students = await _studentRepository.GetAllStudent();
            var subjects = await _subjectRepository.GetAllSubject();
            // Create a ViewModel to hold both lists
            var viewModel = new AddResultViewModel
            {
                Students = students,
                Subjects = subjects
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddResult(ResultViewModel resultViewModel)
        {
            ViewData["result"] = _active;
            if (ModelState.IsValid)
            {
                int mark = (int)resultViewModel.Mark;
                Result result = new()
                {
                    StudentId = (Guid)resultViewModel.StudentId,
                    SubjectId = (Guid)resultViewModel.SubjectId,
                    Mark = mark,
                    Grade = CalculateGrade(mark),
                };
                await _resultRepository.AddResult(result);
                return RedirectToAction("GetResult", "Result", new { id = result.Id });
            }

            var students = await _studentRepository.GetAllStudent();
            var subjects = await _subjectRepository.GetAllSubject();
            // Create a ViewModel to hold both lists
            var viewModel = new AddResultViewModel
            {
                Students = students,
                Subjects = subjects
            };
            return View(viewModel);
        }

        //public async Task<IActionResult> ListResult()
        //{
        //    ViewData["result"] = _active;
        //    IEnumerable<Result>? model = await _resultRepository.GetAllResult();
        //    return View(model);
        //}

        public async Task<IActionResult> ListResult(Class? klass, string? name, string? subject)
        {
            ViewData["result"] = _active;
            ViewData["name"] = name;
            IEnumerable<Result>? model;
            if (klass != null)
            {
                model = await _resultRepository.GetClassResult((Class)klass);
                return View(model);
            }
            else if(name != null)
            {
                model = await _resultRepository.GetStudentResult(name);
                return View(model);
            }
            else if (subject != null)
            {
                model = await _resultRepository.SearchSubjectResult(subject);
                return View(model);
            }
            
            model = await _resultRepository.GetAllResult();
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> GetResult(Guid id)
        {
            ViewData["result"] = _active;
            if (id == null)
            {
                return RedirectToAction("ListResult", "Result");
            }
            if (ModelState.IsValid)
            {
                Result result = await _resultRepository.GetResultById(id);
                if (result == null)
                {
                    Response.StatusCode = 404;
                    return View("StudentNotFound", id);
                }
                return View(result);
            }
            return RedirectToAction("ListResult", "Result");
        }

        [HttpGet]
        public async Task<IActionResult> EditResult(Guid id)
        {
            ViewData["result"] = _active;
            if (id == null)
            {
                return RedirectToAction("StudentList", "Student");
            }
            Result result = await _resultRepository.GetResultById(id);
            if (result == null)
            {
                Response.StatusCode = 404;
                return View("ResultNotFound", id);
            }
            var students = await _studentRepository.GetAllStudent();
            var subjects = await _subjectRepository.GetAllSubject();
            // Create a ViewModel to hold both lists
            var viewModel = new EditResultViewModel
            {
                Students = students,
                Subjects = subjects,
                StudentId = result.Student.Id,
                SubjectId = result.Subject.Id,
                Mark = result.Mark,
                Id = result.Id
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditResult(ResultViewModel model)
        {
            ViewData["result"] = _active;
            if (ModelState.IsValid)
            {
                Result result = await _resultRepository.GetResultById(model.Id);
                result.StudentId = (Guid)model.StudentId;
                result.SubjectId = (Guid)model.SubjectId;
                result.Mark = (int)model.Mark;
                result.Grade = CalculateGrade((int)model.Mark);

                await _resultRepository.UpdateResult(result);
                return RedirectToAction("GetResult", "Result", new { id = model.Id });
            }
            var students = await _studentRepository.GetAllStudent();
            var subjects = await _subjectRepository.GetAllSubject();
            // Create a ViewModel to hold both lists
            var viewModel = new EditResultViewModel
            {
                Students = students,
                Subjects = subjects,
                StudentId = model.StudentId,
                SubjectId = model.SubjectId,
                Mark = model.Mark,
                Id = model.Id
            };

            return View(viewModel);
        }

        public async Task<IActionResult> DeleteResult(Guid id)
        {
            ViewData["result"] = _active;
            if (id == null)
            {
                return RedirectToAction("ListResult", "Result");
            }
            Result result = await _resultRepository.GetResultById(id);
            if (result == null)
            {
                Response.StatusCode = 404;
                return View("ResultNotFound", id);
            }
            await _resultRepository.DeleteResult(id);
            return RedirectToAction("ListResult", "Result");
        }
        private string CalculateGrade(int mark)
        {
            // Implement your logic here to calculate the grade based on the mark
            // For example:
            if (mark >= 90)
            {
                return "A";
            }
            else if (mark >= 80)
            {
                return "B";
            }
            else if (mark >= 70)
            {
                return "C";
            }
            else if (mark >= 60)
            {
                return "D";
            }
            else
            {
                return "F";
            }
        }
    }
}
