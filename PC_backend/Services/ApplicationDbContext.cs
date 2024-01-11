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

    public virtual DbSet<ExamCandAnswer> ExamCandAnswers { get; set; }

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
            entity.HasKey(e => e.CandidateId).HasName("PK__Candidat__DF539BFC22761545");

            entity.HasIndex(e => new { e.FirstName, e.LastName }, "idx_candidate_name");

            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Gender).HasDefaultValueSql("((0))");
            entity.Property(e => e.LandlineNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.NativeLanguage)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Candidate__UserI__403A8C7D");
        });

        modelBuilder.Entity<CandidateAddress>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Candidat__091C2A1B256B1923");

            entity.ToTable("CandidateAddress");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.Address)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(20)
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
                .HasConstraintName("FK__Candidate__Candi__45F365D3");
        });

        modelBuilder.Entity<CandidatePhotoId>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("PK__Candidat__21B7B582D8A506D5");

            entity.ToTable("CandidatePhotoID");

            entity.Property(e => e.PhotoId).HasColumnName("PhotoID");
            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.PhotoIdissueDate)
                .HasColumnType("date")
                .HasColumnName("PhotoIDIssueDate");
            entity.Property(e => e.PhotoIdnumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PhotoIDNumber");
            entity.Property(e => e.PhotoIdtype).HasColumnName("PhotoIDType");

            entity.HasOne(d => d.Candidate).WithMany(p => p.CandidatePhotoIds)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("FK__Candidate__Candi__4316F928");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__Certific__BBF8A7E19C7033DC");

            entity.ToTable("Certificate");

            entity.HasIndex(e => e.Title, "idx_certificate_title");

            entity.Property(e => e.CertificateId).HasColumnName("CertificateID");
            entity.Property(e => e.AssessmentTestCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CertificateTopicMark>(entity =>
        {
            entity.HasKey(e => e.CertificateTopicMarksId).HasName("PK__Certific__2C7F098636C56B22");

            entity.Property(e => e.CertificateTopicMarksId).HasColumnName("CertificateTopicMarksID");
            entity.Property(e => e.CertificateId).HasColumnName("CertificateID");
            entity.Property(e => e.TopicDesc)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Certificate).WithMany(p => p.CertificateTopicMarks)
                .HasForeignKey(d => d.CertificateId)
                .HasConstraintName("FK__Certifica__Certi__4AB81AF0");
        });

        modelBuilder.Entity<EshopProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__EshopPro__B40CC6ED8E9A104C");

            entity.ToTable("EshopProduct");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.ExamId).HasName("PK__Exam__297521A76A438291");

            entity.ToTable("Exam");

            entity.HasIndex(e => e.CandidateId, "idx_exam_candidate");

            entity.Property(e => e.ExamId).HasColumnName("ExamID");
            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.CertificateId).HasColumnName("CertificateID");
            entity.Property(e => e.DateAssigned).HasColumnType("date");

            entity.HasOne(d => d.Candidate).WithMany(p => p.Exams)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("FK__Exam__CandidateI__5070F446");

            entity.HasOne(d => d.Certificate).WithMany(p => p.Exams)
                .HasForeignKey(d => d.CertificateId)
                .HasConstraintName("FK__Exam__Certificat__5165187F");
        });

        modelBuilder.Entity<ExamCandAnswer>(entity =>
        {
            entity.HasKey(e => e.ExamCandAnswerId).HasName("PK__ExamCand__0FEE60D5FD59EFB3");

            entity.ToTable("ExamCandAnswer");

            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.ResultId).HasColumnName("ResultID");

            entity.HasOne(d => d.Question).WithMany(p => p.ExamCandAnswers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__ExamCandA__Quest__5FB337D6");

            entity.HasOne(d => d.Result).WithMany(p => p.ExamCandAnswers)
                .HasForeignKey(d => d.ResultId)
                .HasConstraintName("FK__ExamCandA__Resul__60A75C0F");
        });

        modelBuilder.Entity<ExamResult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__ExamResu__97690228F561245A");

            entity.ToTable("ExamResult");

            entity.Property(e => e.ResultId).HasColumnName("ResultID");
            entity.Property(e => e.ExamId).HasColumnName("ExamID");
            entity.Property(e => e.ResultDate).HasColumnType("date");

            entity.HasOne(d => d.Exam).WithMany(p => p.ExamResults)
                .HasForeignKey(d => d.ExamId)
                .HasConstraintName("FK__ExamResul__ExamI__5441852A");
        });

        modelBuilder.Entity<ExamVoucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__ExamVouc__3AEE79C191182671");

            entity.ToTable("ExamVoucher");

            entity.Property(e => e.VoucherId).HasColumnName("VoucherID");
            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.CertificateId).HasColumnName("CertificateID");
            entity.Property(e => e.ExamDate).HasColumnType("date");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.PurchaseDate).HasColumnType("date");
            entity.Property(e => e.VoucherCode).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Candidate).WithMany(p => p.ExamVouchers)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("FK__ExamVouch__Candi__5BE2A6F2");

            entity.HasOne(d => d.Certificate).WithMany(p => p.ExamVouchers)
                .HasForeignKey(d => d.CertificateId)
                .HasConstraintName("FK__ExamVouch__Certi__5CD6CB2B");

            entity.HasOne(d => d.Product).WithMany(p => p.ExamVouchers)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ExamVouch__Produ__5AEE82B9");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06F8C9CAEFAB8");

            entity.ToTable("Question");

            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.CertificateTopicMarksId).HasColumnName("CertificateTopicMarksID");

            entity.HasOne(d => d.CertificateTopicMarks).WithMany(p => p.Questions)
                .HasForeignKey(d => d.CertificateTopicMarksId)
                .HasConstraintName("FK__Question__Answer__4D94879B");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__UserRole__8AFACE3AB345423D");

            entity.ToTable("UserRole");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleDescription)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usertbl>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Usertbl__1788CCAC262C77D4");

            entity.ToTable("Usertbl");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Cash).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("RoleID");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Usertbls)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Usertbl__RoleID__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
