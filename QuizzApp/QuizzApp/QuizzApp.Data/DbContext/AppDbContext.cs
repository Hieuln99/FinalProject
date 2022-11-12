using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizzApp.Data.Configuration;
using QuizzApp.Data.Entities;
using System;

namespace QuizzApp.Data.DbContext
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfiguration).Assembly);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<UserCoursePayment> UserCoursePayments { get; set; }
        public DbSet<ListApproves> ListApproves { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CategoryCourse> CategoryCourses { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<TestQuestionAnswer> TestQuestionAnswers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TestExam> TestExams { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public override DbSet<User> Users { get; set; }
        
        //public DbSet<Result> Results { get; set; }
    }
}