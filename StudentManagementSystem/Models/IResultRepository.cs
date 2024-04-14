namespace StudentManagementSystem.Models
{
    public interface IResultRepository
    {
        Task<Result> GetResultById(Guid id);

        Task<Result> GetResultByStudentbySubject(Student student, Subject subject);
        Task DeleteResult(Guid id);
        Task<Result> AddResult(Result result);
        Task<Result> UpdateResult(Result result);
        Task<List<Result>> GetAllResult();
        Task<List<Result>> GetClassResult(Class klass);
        Task<List<Result>> GetStudentResult(string name);

        Task<List<Result>> SearchSubjectResult(string subject);
    }
}
