using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace dbmanip.Models{
public class ExamResult
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ResultID { get; set; }
    public int ExamID { get; set; }
    public int CandidateID { get; set; }
    public int Score { get; set; }
    public DateTime ResultDate { get; set; }
    public int Passed { get; set; }
    public int CertificateID { get; set; }

    [ForeignKey("ExamID")]
    public Exam Exam { get; set; }

    [ForeignKey("CandidateID")]
    public Candidates Candidate { get; set; }

    [ForeignKey("CertificateID")]
    public Certificate Certificate { get; set; }
}
}