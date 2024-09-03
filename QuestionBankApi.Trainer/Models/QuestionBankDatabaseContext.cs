using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuestionBankApi.Trainer.Models;

public partial class QuestionBankDatabaseContext : DbContext
{
    public QuestionBankDatabaseContext()
    {
    }

    public QuestionBankDatabaseContext(DbContextOptions<QuestionBankDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PracticePaper> PracticePapers { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<TestPaper> TestPapers { get; set; }

    public virtual DbSet<TestTrainee> TestTrainees { get; set; }

    public virtual DbSet<UserTbl> UserTbls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=FNFIDVPRE20501\\SQL2022; Database=QuestionBankDatabase; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PracticePaper>(entity =>
        {
            entity.HasKey(e => e.PaperId).HasName("PK__Practice__AB86120B9BD8BBEA");

            entity.ToTable("PracticePaper");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaperName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PracticePapers)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__PracticeP__Creat__571DF1D5");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06FAC6AC959D8");

            entity.ToTable("Question");

            entity.Property(e => e.CorrectAnswer)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DifficultyLevel)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Explaination)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.OptionA)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OptionB)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OptionC)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OptionD)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.QuestionText).HasColumnType("text");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Topic)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Questions)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Question__Create__412EB0B6");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__Result__976902082336CB54");

            entity.ToTable("Result");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Percentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Score).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Test).WithMany(p => p.Results)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK__Result__TestId__52593CB8");

            entity.HasOne(d => d.User).WithMany(p => p.Results)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Result__UserId__534D60F1");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__Test__8CC3316068BD5A68");

            entity.ToTable("Test");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpiryTime).HasColumnType("datetime");
            entity.Property(e => e.Hyperlinks)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.TestName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Tests)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Test__CreatedBy__44FF419A");
        });

        modelBuilder.Entity<TestPaper>(entity =>
        {
            entity.HasKey(e => e.TestPaperId).HasName("PK__TestPape__0D72A566281D1034");

            entity.ToTable("TestPaper");

            entity.Property(e => e.TestPath)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Test).WithMany(p => p.TestPapers)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK__TestPaper__TestI__47DBAE45");
        });

        modelBuilder.Entity<TestTrainee>(entity =>
        {
            entity.HasKey(e => e.TestTraineeId).HasName("PK__TestTrai__2F9F0C6982A9FDF4");

            entity.ToTable("TestTrainee");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Score).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Registered");

            entity.HasOne(d => d.Test).WithMany(p => p.TestTrainees)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK__TestTrain__TestI__4D94879B");

            entity.HasOne(d => d.User).WithMany(p => p.TestTrainees)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__TestTrain__UserI__4E88ABD4");
        });

        modelBuilder.Entity<UserTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserTbl__3214EC07DF68892B");

            entity.ToTable("UserTbl");

            entity.HasIndex(e => e.Email, "UQ__UserTbl__A9D1053473E5FB42").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Is2Faenabled)
                .HasDefaultValue(false)
                .HasColumnName("Is2FAEnabled");
            entity.Property(e => e.IsAdminApproved).HasDefaultValue(false);
            entity.Property(e => e.IsVerified).HasDefaultValue(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
