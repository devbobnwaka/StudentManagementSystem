using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Subject
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public ICollection<Result> Results { get; } = new List<Result>(); //Collection navigation containing dependents
    }
}
