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

    public virtual DbSet<UserCertificatePurchase> UserCertificatePurchases { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Usertbl> Usertbls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:MySqlConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.CandidateId).HasName("PK__Candidat__DF539BFC7405098D");

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
            entity.HasKey(e => e.AddressId).HasName("PK__Candidat__091C2A1BC046CB31");

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
            entity.HasKey(e => e.PhotoId).HasName("PK__Candidat__21B7B5828E300789");

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
            entity.HasKey(e => e.CertificateId).HasName("PK__Certific__BBF8A7E1C31C1AE2");

            entity.ToTable("Certificate");

            entity.HasIndex(e => e.Title, "idx_certificate_title");

            entity.Property(e => e.CertificateId).HasColumnName("CertificateID");
            entity.Property(e => e.AssessmentTestCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CertificateTopicMark>(entity =>
        {
            entity.HasKey(e => e.CertificateTopicMarksId).HasName("PK__Certific__2C7F0986886A2470");

            entity.Property(e => e.CertificateTopicMarksId).HasColumnName("CertificateTopicMarksID");
            entity.Property(e => e.CertificateId).HasColumnName("CertificateID");
            entity.Property(e => e.NumberOfAwardedMarks).HasDefaultValueSql("((0))");
            entity.Property(e => e.NumberOfPossibleMarks).HasDefaultValueSql("((0))");
            entity.Property(e => e.TopicDesc)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Certificate).WithMany(p => p.CertificateTopicMarks)
                .HasForeignKey(d => d.CertificateId)
                .HasConstraintName("FK__Certifica__Certi__4D94879B");
        });

        modelBuilder.Entity<EshopProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__EshopPro__B40CC6ED35258E8D");

            entity.ToTable("EshopProduct");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CertificateId).HasColumnName("CertificateID");
            entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Certificate).WithMany(p => p.EshopProducts)
                .HasForeignKey(d => d.CertificateId)
                .HasConstraintName("FK__EshopProd__Delet__5441852A");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.ExamId).HasName("PK__Exam__297521A7F6D94E92");

            entity.ToTable("Exam");

            entity.HasIndex(e => e.CandidateId, "idx_exam_candidate");

            entity.Property(e => e.ExamId).HasColumnName("ExamID");
            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.DateAssigned).HasColumnType("datetime");
            entity.Property(e => e.VoucherId).HasColumnName("VoucherID");

            entity.HasOne(d => d.Candidate).WithMany(p => p.Exams)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("FK__Exam__CandidateI__5BE2A6F2");

            entity.HasOne(d => d.Voucher).WithMany(p => p.Exams)
                .HasForeignKey(d => d.VoucherId)
                .HasConstraintName("FK__Exam__VoucherID__5CD6CB2B");
        });

        modelBuilder.Entity<ExamCandAnswer>(entity =>
        {
            entity.HasKey(e => e.ExamCandAnswerId).HasName("PK__ExamCand__0FEE60D57C0E3F17");

            entity.ToTable("ExamCandAnswer");

            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.ResultId).HasColumnName("ResultID");

            entity.HasOne(d => d.Question).WithMany(p => p.ExamCandAnswers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__ExamCandA__Quest__66603565");

            entity.HasOne(d => d.Result).WithMany(p => p.ExamCandAnswers)
                .HasForeignKey(d => d.ResultId)
                .HasConstraintName("FK__ExamCandA__Resul__6754599E");
        });

        modelBuilder.Entity<ExamResult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__ExamResu__97690228894DE477");

            entity.ToTable("ExamResult");

            entity.Property(e => e.ResultId).HasColumnName("ResultID");
            entity.Property(e => e.ExamId).HasColumnName("ExamID");
            entity.Property(e => e.ResultDate).HasColumnType("datetime");

            entity.HasOne(d => d.Exam).WithMany(p => p.ExamResults)
                .HasForeignKey(d => d.ExamId)
                .HasConstraintName("FK__ExamResul__ExamI__5FB337D6");
        });

        modelBuilder.Entity<ExamVoucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__ExamVouc__3AEE79C1A13BE1C1");

            entity.ToTable("ExamVoucher");

            entity.Property(e => e.VoucherId).HasColumnName("VoucherID");
            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.CertificateId).HasColumnName("CertificateID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.VoucherCode).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Candidate).WithMany(p => p.ExamVouchers)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("FK__ExamVouch__Candi__59063A47");

            entity.HasOne(d => d.Product).WithMany(p => p.ExamVouchers)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ExamVouch__Produ__5812160E");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06F8C4FE2B608");

            entity.ToTable("Question");

            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.CertificateTopicMarksId).HasColumnName("CertificateTopicMarksID");

            entity.HasOne(d => d.CertificateTopicMarks).WithMany(p => p.Questions)
                .HasForeignKey(d => d.CertificateTopicMarksId)
                .HasConstraintName("FK__Question__Answer__5070F446");
        });

        modelBuilder.Entity<UserCertificatePurchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK__UserCert__6B0A6BDE01E566C9");

            entity.ToTable("UserCertificatePurchase");

            entity.Property(e => e.PurchaseId).HasColumnName("PurchaseID");
            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.PurchaseDate).HasColumnType("date");

            entity.HasOne(d => d.Candidate).WithMany(p => p.UserCertificatePurchases)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("FK__UserCerti__Candi__628FA481");

            entity.HasOne(d => d.Product).WithMany(p => p.UserCertificatePurchases)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__UserCerti__Produ__6383C8BA");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__UserRole__8AFACE3A24116D85");

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
            entity.HasKey(e => e.UserId).HasName("PK__Usertbl__1788CCACBB701B35");

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
