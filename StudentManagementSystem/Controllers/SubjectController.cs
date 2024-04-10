using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Dto;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class SubjectController : Controller
    {
        private readonly string _active = "active";
        private readonly ISubjectRepository _subjectRepository;

        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        [HttpGet]
        public async Task<IActionResult> ListSubject()
        {
            ViewData["subject"] = _active;
            List<Subject> subjects = await _subjectRepository.GetAllSubject();
            return View(subjects);
        }

        [HttpGet]
        public async Task<IActionResult> GetSubject(Guid? id)
        {
            ViewData["subject"] = _active;
            if (id == null)
            {
                return RedirectToAction("ListSubject", "Subject");
            }
            if (ModelState.IsValid)
            {
                Subject subject = await _subjectRepository.GetSubjectById(id);
                if (subject == null)
                {
                    Response.StatusCode = 404;
                    return View("SubjectNotFound", id);
                }
                Subject subjectDetailsViewModel = new()
                {
                    Name = subject.Name,
                };
                return View(subjectDetailsViewModel);
            }
            return RedirectToAction("ListSubject", "Subject");
        }

        [HttpGet]
        public IActionResult CreateSubject()
        {
            ViewData["subject"] = _active;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditSubject(Guid? id)
        {
            ViewData["subject"] = _active;
            if (id == null)
            {
                return RedirectToAction("ListSubject", "Subject");
            }
            Subject subject = await _subjectRepository.GetSubjectById(id);
            if (subject == null)
            {
                Response.StatusCode = 404;
                return View("SubjectNotFound", id);
            }
            return View(subject);
        }

        [HttpPost]
        public async Task<IActionResult> EditSubject([Bind("Name", "Id")] Subject? model)
        {
            ViewData["student"] = _active;
            if (ModelState.IsValid)
            {
                Subject subject = await _subjectRepository.GetSubjectById(model.Id);
                subject.Name = model.Name;

                await _subjectRepository.UpdateSubject(subject);
                return RedirectToAction("ListSubject", "Subject");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject([Bind("Name")]Subject? model)
        {
            ViewData["subject"] = _active;
            if (ModelState.IsValid)
            {
                Subject subject = new()
                {
                    Name = model.Name,
                };
                await _subjectRepository.CreateSubject(subject);
                return RedirectToAction("ListSubject", "Subject");
            }
            return View();
        }

        public async Task<IActionResult> DeleteSubject(Guid? id)
        {
            ViewData["subject"] = _active;
            if (id == null)
            {
                return RedirectToAction("ListSubject", "Subject");
            }
            Subject subject = await _subjectRepository.GetSubjectById(id);
            if (subject == null)
            {
                Response.StatusCode = 404;
                return View("SubjectNotFound", id);
            }
            await _subjectRepository.DeleteSubject(id);
            return RedirectToAction("ListSubject", "Subject");
        }
    }
}
