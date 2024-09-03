using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainerBackendDll.Dtos;

namespace QuestionBankDll.Trainer.Models
{
    public class Question
    {
        public int QuestionId { get; set; }

        public string Subject { get; set; }

        public string Topic { get; set; }

        public string DifficultyLevel { get; set; }

        public string QuestionText { get; set; }

        public string OptionA { get; set; }


        public string OptionB { get; set; }

        public string OptionC { get; set; }

        public string OptionD { get; set; }

        public string CorrectAnswer { get; set; }
        public string Explaination { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<TestQuestion>? TestQuestions { get; set; }

    }
    public class QuestionDbContext : DbContext
    {
        public QuestionDbContext()
        {
        }

        public QuestionDbContext(DbContextOptions<QuestionDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("dbSettings.json").Build();
                string connectionString = configuration.GetConnectionString("myConnection")!;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public DbSet<PracticePaper> PracticePapers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestTrainee> TestTrainees { get; set; }
        public DbSet<TestPaper> TestPapers { get; set; }
        public DbSet<UserTbl> UserTbls { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<TestResult> TestResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().ToTable("Question");
            modelBuilder.Entity<UserAnswer>().ToTable("UserAnswer");
            modelBuilder.Entity<TestResult>().ToTable("TestResult");

            modelBuilder.Entity<PracticePaper>().HasKey(p => p.PaperId);
            modelBuilder.Entity<PracticePaper>().ToTable("PracticePaper");
            modelBuilder.Entity<Test>().ToTable("Test");
            modelBuilder.Entity<TestQuestion>().ToTable("TestQuestion");
            modelBuilder.Entity<TestQuestion>()
            .HasKey(tq => new { tq.TestId, tq.QuestionId });

            modelBuilder.Entity<TestQuestion>()
                .HasOne(tq => tq.Test)
                .WithMany(t => t.TestQuestions)
                .HasForeignKey(tq => tq.TestId);

            modelBuilder.Entity<TestQuestion>()
                .HasOne(tq => tq.Question)
                .WithMany(q => q.TestQuestions)
                .HasForeignKey(tq => tq.QuestionId);

        }

    }


}

