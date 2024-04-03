
using Microsoft.EntityFrameworkCore;

namespace StudentManagementSystem.Models
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _dbContext;
        public SubjectRepository(AppDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }
        public async Task<Subject> CreateSubject(Subject subject)
        {
            await _dbContext.Subjects.AddAsync(subject);
            await _dbContext.SaveChangesAsync();
            return subject;
        }

        public Task DeleteSubject(Guid? id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Subject>> GetAllSubject()
        {
            return await _dbContext.Subjects.ToListAsync();
        }

        public async Task<Subject> GetSubjectById(Guid? id)
        {
            return await _dbContext.Subjects.FindAsync(id);
        }

        public Task<Subject> UpdateSubject(Subject subject)
        {
            throw new NotImplementedException();
        }
    }
}
