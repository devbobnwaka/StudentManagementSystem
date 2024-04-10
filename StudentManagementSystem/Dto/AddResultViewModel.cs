using StudentManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Dto
{
    public class AddResultViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
        public ResultViewModel ResultViewModel { get; set; } // To hold the result object in case of validation errors
    }
}
