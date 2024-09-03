using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TraineeBackendDll.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TraineeBackendDll.Data
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

        public DbSet<TestQuestion> TestQuestions { get; set; }

       // public DbSet<TestAnswer> testAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure table names if different from default
            modelBuilder.Entity<Question>().ToTable("Question");  // Map to the correct table name
            modelBuilder.Entity<Test>().ToTable("Test");
            modelBuilder.Entity<Result>().ToTable("Result");

            modelBuilder.Entity<Test>()
            .HasKey(t => t.TestId);

            modelBuilder.Entity<TestTrainee>()
            .Property(t => t.Score)
            .HasColumnType("decimal(5,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
