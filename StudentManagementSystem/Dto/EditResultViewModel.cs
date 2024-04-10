using StudentManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Dto
{
    public class EditResultViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
        [Required]
        public Guid Id { get; set; }
        [Display(Name = "Student")]
        [Required]
        public Guid? StudentId { get; set; } //Required foreign key property
        [Display(Name = "Subject")]
        [Required]
        public Guid? SubjectId { get; set; }
        [Required]
        public int? Mark { get; set; }
    }
}
