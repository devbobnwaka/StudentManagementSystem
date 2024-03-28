using Microsoft.EntityFrameworkCore;

namespace StudentManagementSystem.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(e => e.Results)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired();

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Results)
                .WithOne(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .IsRequired();
        }

    }
}
