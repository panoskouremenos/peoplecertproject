using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace dbmanip.Models{
public class Certificate
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CertificateID { get; set; }
    public string Title { get; set; }
    public int CandidateID { get; set; }
    public string AssessmentTestCode { get; set; }
    public DateTime ExaminationDate { get; set; }
    public DateTime ScoreReportDate { get; set; }
    public int CandidateScore { get; set; }
    public int MaximumScore { get; set; }
    public float PercentageScore { get; set; }
    public string AssessmentResultLabel { get; set; }

    [ForeignKey("CandidateID")]
    public Candidates Candidate { get; set; }
}
}