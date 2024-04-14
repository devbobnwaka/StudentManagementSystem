
using Microsoft.EntityFrameworkCore;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace StudentManagementSystem.Models
{
    public class ResultRepository : IResultRepository
    {
        private readonly AppDbContext _dbContext;

        public ResultRepository(AppDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }
        public async Task<Result> AddResult(Result result)
        {
            await _dbContext.Results.AddAsync(result);
            await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task DeleteResult(Guid id)
        {
            Result? result = await _dbContext.Results.FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                _dbContext.Results.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Result>> GetAllResult()
        {
            return await _dbContext.Results.Include(e => e.Student).Include(e => e.Subject).ToListAsync();
        }

        public async Task<List<Result>> GetClassResult(Class klass)
        {
            return await _dbContext.Results
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .Where(e => e.Student.Class == klass)
                .ToListAsync();
        }

        public async Task<Result> GetResultById(Guid id)
        {
            return await _dbContext.Results
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Result> GetResultByStudentbySubject(Student student, Subject subject)
        {
            return await _dbContext.Results
                        .Include(e => e.Student)
                        .Include(e => e.Subject)
                        .FirstOrDefaultAsync(e => e.Student == student && e.Subject == subject);
        }

        public async Task<List<Result>> GetStudentResult(string name)
        {
            return await _dbContext.Results
               .Include(e => e.Student)
               .Include(e => e.Subject)
               .Where(e => e.Student.Name.Contains(name))
               .ToListAsync();
        }

        public async Task<List<Result>> SearchSubjectResult(string subject)
        {
            return await _dbContext.Results
               .Include(e => e.Student)
               .Include(e => e.Subject)
               .Where(e => e.Subject.Name == subject)
               .ToListAsync();
        }

        public async Task<Result> UpdateResult(Result result)
        {
            _dbContext.Entry(result).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return result;
        }
    }
}
