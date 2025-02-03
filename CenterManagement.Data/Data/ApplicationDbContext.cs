using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CenterManagement.Models;

namespace CenterManagement.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Student>()
              .HasKey(b => b.Id);

            builder.Entity<Exam>()
             .HasKey(b => b.Id);

            builder.Entity<Teacher>()
              .HasKey(b => b.Id);

            builder.Entity<Barcode>()
              .HasKey(b => b.barcode);

            builder.Entity<Lesson>()
              .HasKey(b => b.Id);

            builder.Entity<Question>()
           .HasKey(b => new { b.Numbre, b.ExamId });

            builder.Entity<Result>()
           .HasKey(b => new { b.StudentId, b.ExamId });

            builder.Entity<LessonTask>()
           .HasKey(b => new { b.Question, b.LessonId });

            builder.Entity<Students_Task>()
           .HasKey(b => new { b.LessonId, b.StudentId });

        }


        public DbSet<Student> student { get; set; }

        public DbSet<Teacher> teachers { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Exam> Exams { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Result> Results { get; set; }

        public DbSet<Barcode> Barcodes { get; set; }

        public DbSet<LessonTask> lessonTasks { get; set; }

        public DbSet<Students_Task> StudentTasks { get; set; }
    }
}