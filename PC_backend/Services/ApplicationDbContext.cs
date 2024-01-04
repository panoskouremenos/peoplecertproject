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
        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.CandidateId).HasName("PK__Candidat__DF539BFC121D737A");

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
                .HasConstraintName("FK__Candidate__UserI__3C69FB99");
        });

        modelBuilder.Entity<CandidateAddress>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Candidat__091C2A1BA43A1709");

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
                .HasConstraintName("FK__Candidate__Candi__4222D4EF");
        });

        modelBuilder.Entity<CandidatePhotoId>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("PK__Candidat__21B7B582CF74ADC7");

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
            entity.Property(e => e.PhotoIdtype).HasColumnName("PhotoIDType");

            entity.HasOne(d => d.Candidate).WithMany(p => p.CandidatePhotoIds)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("FK__Candidate__Candi__3F466844");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__Certific__BBF8A7E121A91448");

            entity.ToTable("Certificate");

            entity.HasIndex(e => e.Title, "idx_certificate_title");

            entity.Property(e => e.CertificateId).HasColumnName("CertificateID");
            entity.Property(e => e.AssessmentTestCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CertificateTopicMark>(entity =>
        {
            entity.HasKey(e => e.CertificateTopicMarksId).HasName("PK__Certific__2C7F09860032D521");

            entity.Property(e => e.CertificateTopicMarksId).HasColumnName("CertificateTopicMarksID");
            entity.Property(e => e.CertificateId).HasColumnName("CertificateID");
            entity.Property(e => e.TopicDesc)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Certificate).WithMany(p => p.CertificateTopicMarks)
                .HasForeignKey(d => d.CertificateId)
                .HasConstraintName("FK__Certifica__Certi__46E78A0C");
        });

        modelBuilder.Entity<EshopProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__EshopPro__B40CC6ED542DDB19");

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
            entity.HasKey(e => e.ExamId).HasName("PK__Exam__297521A7A509C410");

            entity.ToTable("Exam");

            entity.HasIndex(e => e.CandidateId, "idx_exam_candidate");

            entity.Property(e => e.ExamId).HasColumnName("ExamID");
            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.CertificateId).HasColumnName("CertificateID");
            entity.Property(e => e.DateAssigned).HasColumnType("date");

            entity.HasOne(d => d.Candidate).WithMany(p => p.Exams)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("FK__Exam__CandidateI__4CA06362");

            entity.HasOne(d => d.Certificate).WithMany(p => p.Exams)
                .HasForeignKey(d => d.CertificateId)
                .HasConstraintName("FK__Exam__Certificat__4D94879B");
        });

        modelBuilder.Entity<ExamResult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__ExamResu__976902284D2116B9");

            entity.ToTable("ExamResult");

            entity.Property(e => e.ResultId).HasColumnName("ResultID");
            entity.Property(e => e.ExamId).HasColumnName("ExamID");
            entity.Property(e => e.ResultDate).HasColumnType("date");

            entity.HasOne(d => d.Exam).WithMany(p => p.ExamResults)
                .HasForeignKey(d => d.ExamId)
                .HasConstraintName("FK__ExamResul__ExamI__5070F446");
        });

        modelBuilder.Entity<ExamVoucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__ExamVouc__3AEE79C12EFD2525");

            entity.ToTable("ExamVoucher");

            entity.Property(e => e.VoucherId).HasColumnName("VoucherID");
            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.ExamDate).HasColumnType("date");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.PurchaseDate).HasColumnType("date");

            entity.HasOne(d => d.Candidate).WithMany(p => p.ExamVouchers)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("FK__ExamVouch__Candi__5629CD9C");

            entity.HasOne(d => d.Product).WithMany(p => p.ExamVouchers)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ExamVouch__Produ__5535A963");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06F8CC0F70915");

            entity.ToTable("Question");

            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.Answer).HasColumnType("text");
            entity.Property(e => e.CertificateTopicMarksId).HasColumnName("CertificateTopicMarksID");
            entity.Property(e => e.QuestionText).HasColumnType("text");
            entity.Property(e => e.QuestionType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CertificateTopicMarks).WithMany(p => p.Questions)
                .HasForeignKey(d => d.CertificateTopicMarksId)
                .HasConstraintName("FK__Question__Answer__49C3F6B7");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__UserRole__8AFACE3AE756E80B");

            entity.ToTable("UserRole");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleDescription).HasColumnType("text");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usertbl>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Usertbl__1788CCAC1179DB4B");

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
                .HasConstraintName("FK__Usertbl__RoleID__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
