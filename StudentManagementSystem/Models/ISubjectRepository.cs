namespace StudentManagementSystem.Models
{
    public interface ISubjectRepository
    {
        Task<Subject> GetSubjectById(Guid? id);
        Task DeleteSubject(Guid? id);
        Task<Subject> CreateSubject(Subject subject);
        Task<Subject> UpdateSubject(Subject subject);
        Task<List<Subject>> GetAllSubject();
    }
}
