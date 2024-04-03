using Microsoft.EntityFrameworkCore;

namespace StudentManagementSystem.Models
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _dbContext;

        public StudentRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<Student> CreateStudent(Student student)
        {
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return student;
        }

        public async Task DeleteStudent(Guid? id)
        {
            Student? student = await _dbContext.Students.FirstOrDefaultAsync(e => e.Id == id);
            if (student != null)
            {
                _dbContext.Students.Remove(student);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudent()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<Student?> GetStudentById(Guid? id)
        {
            return await _dbContext.Students.FindAsync(id);
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            _dbContext.Entry(student).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return student;
        }
    }
}
