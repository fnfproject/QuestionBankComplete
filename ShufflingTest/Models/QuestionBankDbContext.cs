using Microsoft.EntityFrameworkCore;
using TrainerBackendDll.Models;

namespace ShufflingTest.Models
{
    public class QuestionBankDbContext : DbContext
    {
        public QuestionBankDbContext(DbContextOptions<QuestionBankDbContext> options) : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Result> Results { get; set; }

        public DbSet<TestTrainee> TestTrainees { get; set; }

        public DbSet<UserTbl> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure table names if different from default
            modelBuilder.Entity<Question>().ToTable("Question");  // Map to the correct table name
            modelBuilder.Entity<Test>().ToTable("Test");
            modelBuilder.Entity<Result>().ToTable("Result");

            base.OnModelCreating(modelBuilder);
        }
    }
}
