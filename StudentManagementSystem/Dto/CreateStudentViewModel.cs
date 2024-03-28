using StudentManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Dto
{
    public class CreateStudentViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public Class? Class { get; set; }
       
        [Display(Name = "Contact Information")]
        public string ContactInformation { get; set; } = null!;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        [Required]
        public DateTime? DateOfBirth { get; set; }
    }
}
