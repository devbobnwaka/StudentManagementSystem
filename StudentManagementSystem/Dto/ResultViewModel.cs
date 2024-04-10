using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Dto
{
    public class ResultViewModel
    {
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
