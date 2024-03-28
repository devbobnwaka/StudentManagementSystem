using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Result
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; } //Required foreign key property
        public Guid SubjectId { get; set; }
        [Required]
        public int Mark { get; set; }
        public string Grade { get; set; } = null!;

        // Navigation properties
        public Student Student { get; set; } = null!; //Required reference navigation to principal
        public Subject Subject { get; set; } = null!;
    }
}
