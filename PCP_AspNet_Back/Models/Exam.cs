using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace dbmanip.Models{
public class Exam
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ExamID { get; set; }
    public int CandidateID { get; set; }
    public string TestCode { get; set; }
    public DateTime DateTaken { get; set; }
    public int TotalScore { get; set; }
    public int PassingMark { get; set; }

    [ForeignKey("CandidateID")]
    public Candidates Candidate { get; set; }
}
}