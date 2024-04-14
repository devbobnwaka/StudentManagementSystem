using System.Threading.Tasks;

namespace StudentManagementSystem.Models
{
    public interface IStudentRepository
    {
        Task<Student> GetStudentById(Guid? id);
        Task DeleteStudent(Guid? id);
        Task<Student> CreateStudent(Student student);
        Task<Student> UpdateStudent(Student student);
        Task<IEnumerable<Student>> GetAllStudent();

        Task<List<Student>> GetClassStudent(Class klass);

        Task<Student?> GetStudentByName(string name);
    }
}
