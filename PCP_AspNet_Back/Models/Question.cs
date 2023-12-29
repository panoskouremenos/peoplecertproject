
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace dbmanip.Models{
public class Question
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int QuestionID { get; set; }
    public int ExamID { get; set; }
    public string QuestionText { get; set; }
    public string QuestionType { get; set; }

    [ForeignKey("ExamID")]
    public Exam Exam { get; set; }
}
}