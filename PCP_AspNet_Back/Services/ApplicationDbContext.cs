using Microsoft.EntityFrameworkCore;
using dbmanip.Models;

namespace dbmanip.Services{
public class ApplicationDbContext : DbContext
{
    public DbSet<UserRole> tbl_UserRole { get; set; }
    public DbSet<Usertbl> tbl_Usertbl { get; set; }
    public DbSet<Candidates> tbl_Candidates { get; set; }
    public DbSet<CandidatePhotoID> tbl_CandidatePhotoID { get; set; }
    public DbSet<CandidateAddress> tbl_CandidateAddress { get; set; }
    public DbSet<Certificate> tbl_Certificate { get; set; }
    public DbSet<CertificateTopicMarks> tbl_CertificateTopicMarks { get; set; }
    public DbSet<Exam> tbl_Exam { get; set; }
    public DbSet<Question> tbl_Question { get; set; }
    public DbSet<Answer> tbl_Answer { get; set; }
    public DbSet<ExamResult> tbl_ExamResult { get; set; }
    public DbSet<EshopProduct> tbl_EshopProduct { get; set; }
    public DbSet<ExamVoucher> tbl_ExamVoucher { get; set; }
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRole>().ToTable("tbl_UserRole");
        modelBuilder.Entity<Usertbl>().ToTable("tbl_Usertbl");
        modelBuilder.Entity<Candidates>().ToTable("tbl_Candidates");
        modelBuilder.Entity<CandidatePhotoID>().ToTable("tbl_CandidatePhotoID");
        modelBuilder.Entity<CandidateAddress>().ToTable("tbl_CandidateAddress");
        modelBuilder.Entity<Certificate>().ToTable("tbl_Certificate");
        modelBuilder.Entity<CertificateTopicMarks>().ToTable("tbl_CertificateTopicMarks");
        modelBuilder.Entity<Exam>().ToTable("tbl_Exam");
        modelBuilder.Entity<Question>().ToTable("tbl_Question");
        modelBuilder.Entity<Answer>().ToTable("tbl_Answer");
        modelBuilder.Entity<ExamResult>().ToTable("tbl_ExamResult");
        modelBuilder.Entity<EshopProduct>().ToTable("tbl_EshopProduct");
        modelBuilder.Entity<ExamVoucher>().ToTable("tbl_ExamVoucher");

        modelBuilder.Entity<Candidates>()
            .HasIndex(c => new { c.FirstName, c.LastName })
            .HasName("idx_candidate_name");

        modelBuilder.Entity<Candidates>()
            .HasIndex(c => c.Email)
            .HasName("idx_candidate_email");

        modelBuilder.Entity<Certificate>()
            .HasIndex(c => c.Title)
            .HasName("idx_certificate_title");

        modelBuilder.Entity<Exam>()
            .HasIndex(e => e.DateTaken)
            .HasName("idx_exam_date");

        modelBuilder.Entity<Certificate>()
            .HasIndex(c => c.CandidateID)
            .HasName("idx_certificate_candidate");

        modelBuilder.Entity<Exam>()
            .HasIndex(e => e.CandidateID)
            .HasName("idx_exam_candidate");

        modelBuilder.Entity<Question>()
            .HasIndex(q => q.ExamID)
            .HasName("idx_question_exam");

        modelBuilder.Entity<Answer>()
            .HasIndex(a => a.QuestionID)
            .HasName("idx_answer_question");
    }
}
}