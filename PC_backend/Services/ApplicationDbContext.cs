using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PC_backend.Models;

namespace PC_backend.Services;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<CandidateAddress> CandidateAddresses { get; set; }

    public virtual DbSet<CandidatePhotoId> CandidatePhotoIds { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<CertificateTopicMark> CertificateTopicMarks { get; set; }

    public virtual DbSet<EshopProduct> EshopProducts { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<ExamResult> ExamResults { get; set; }

    public virtual DbSet<ExamVoucher> ExamVouchers { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Usertbl> Usertbls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:MySqlConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Answer__D482502411C318F2");

            entity.ToTable("Answer");

            entity.HasIndex(e => e.QuestionId, "idx_answer_question");

            entity.Property(e => e.AnswerId).HasColumnName("AnswerID");
            entity.Property(e => e.AnswerText).HasColumnType("text");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__Answer__Question__57DD0BE4");
        });

        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.CandidateId).HasName("PK__Candidat__DF539BFCC8236A3C");

            entity.HasIndex(e => e.Email, "idx_candidate_email");

            entity.HasIndex(e => new { e.FirstName, e.LastName }, "idx_candidate_name");

            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LandlineNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NativeLanguage)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Candidate__UserI__43D61337");
        });

        modelBuilder.Entity<CandidateAddress>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Candidat__091C2A1BF29327D8");

            entity.ToTable("CandidateAddress");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.CountryOfResidence)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.StateTerritoryProvince)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TownCity)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Candidate).WithMany(p => p.CandidateAddresses)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("FK__Candidate__Candi__498EEC8D");
        });

        modelBuilder.Entity<CandidatePhotoId>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("PK__Candidat__21B7B5824D841CCC");

            entity.ToTable("CandidatePhotoID");

            entity.Property(e => e.PhotoId).HasColumnName("PhotoID");
            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.PhotoIdissueDate)
                .HasColumnType("date")
                .HasColumnName("PhotoIDIssueDate");
            entity.Property(e => e.PhotoIdnumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PhotoIDNumber");
            entity.Property(e => e.PhotoIdtype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PhotoIDType");

            entity.HasOne(d => d.Candidate).WithMany(p => p.CandidatePhotoIds)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("FK__Candidate__Candi__46B27FE2");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__Certific__BBF8A7E194BC76C0");

            entity.ToTable("Certificate");

            entity.HasIndex(e => e.CandidateId, "idx_certificate_candidate");

            entity.HasIndex(e => e.Title, "idx_certificate_title");

            entity.Property(e => e.CertificateId).HasColumnName("CertificateID");
            entity.Property(e => e.AssessmentResultLabel)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AssessmentTestCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.ExaminationDate).HasColumnType("date");
            entity.Property(e => e.ScoreReportDate).HasColumnType("date");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Candidate).WithMany(p => p.Certificates)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("FK__Certifica__Candi__4C6B5938");
        });

        modelBuilder.Entity<CertificateTopicMark>(entity =>
        {
            entity.HasKey(e => e.CertificateTopicMarksId).HasName("PK__Certific__2C7F0986A9C2E005");

            entity.Property(e => e.CertificateTopicMarksId).HasColumnName("CertificateTopicMarksID");
            entity.Property(e => e.CertificateId).HasColumnName("CertificateID");
            entity.Property(e => e.TopicDesc)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Certificate).WithMany(p => p.CertificateTopicMarks)
                .HasForeignKey(d => d.CertificateId)
                .HasConstraintName("FK__Certifica__Certi__4F47C5E3");
        });

        modelBuilder.Entity<EshopProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__EshopPro__B40CC6ED8F2F8F97");

            entity.ToTable("EshopProduct");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.ExamId).HasName("PK__Exam__297521A754380D3D");

            entity.ToTable("Exam");

            entity.HasIndex(e => e.CandidateId, "idx_exam_candidate");

            entity.HasIndex(e => e.DateTaken, "idx_exam_date");

            entity.Property(e => e.ExamId).HasColumnName("ExamID");
            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.DateTaken).HasColumnType("date");
            entity.Property(e => e.TestCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Candidate).WithMany(p => p.Exams)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("FK__Exam__CandidateI__5224328E");
        });

        modelBuilder.Entity<ExamResult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__ExamResu__97690228236B9C66");

            entity.ToTable("ExamResult");

            entity.Property(e => e.ResultId).HasColumnName("ResultID");
            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.CertificateId).HasColumnName("CertificateID");
            entity.Property(e => e.ExamId).HasColumnName("ExamID");
            entity.Property(e => e.ResultDate).HasColumnType("date");

            entity.HasOne(d => d.Candidate).WithMany(p => p.ExamResults)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("FK__ExamResul__Candi__5BAD9CC8");

            entity.HasOne(d => d.Certificate).WithMany(p => p.ExamResults)
                .HasForeignKey(d => d.CertificateId)
                .HasConstraintName("FK__ExamResul__Certi__5CA1C101");

            entity.HasOne(d => d.Exam).WithMany(p => p.ExamResults)
                .HasForeignKey(d => d.ExamId)
                .HasConstraintName("FK__ExamResul__ExamI__5AB9788F");
        });

        modelBuilder.Entity<ExamVoucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__ExamVouc__3AEE79C17C99702A");

            entity.ToTable("ExamVoucher");

            entity.Property(e => e.VoucherId).HasColumnName("VoucherID");
            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.ExamDate).HasColumnType("date");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.PurchaseDate).HasColumnType("date");

            entity.HasOne(d => d.Candidate).WithMany(p => p.ExamVouchers)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("FK__ExamVouch__Candi__625A9A57");

            entity.HasOne(d => d.Product).WithMany(p => p.ExamVouchers)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ExamVouch__Produ__6166761E");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06F8C7ACDC3F7");

            entity.ToTable("Question");

            entity.HasIndex(e => e.ExamId, "idx_question_exam");

            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.ExamId).HasColumnName("ExamID");
            entity.Property(e => e.QuestionText).HasColumnType("text");
            entity.Property(e => e.QuestionType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Exam).WithMany(p => p.Questions)
                .HasForeignKey(d => d.ExamId)
                .HasConstraintName("FK__Question__ExamID__55009F39");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__UserRole__8AFACE3A66966242");

            entity.ToTable("UserRole");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleDescription).HasColumnType("text");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usertbl>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Usertbl__1788CCACB137FABB");

            entity.ToTable("Usertbl");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Usertbls)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Usertbl__RoleID__40F9A68C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
