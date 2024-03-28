using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace StudentManagementSystem.Models
{
    public enum Class
    {
        JSS1,
        JSS2,
        JSS3,
        SSS1,
        SSS2,
        SSS3
    }
    public class Student
    {
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public Class? Class { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }
        public string ContactInformation { get; set; } = null!;

        public ICollection<Result> Results { get;} = new List<Result>(); //Collection navigation containing dependents

    }
}
