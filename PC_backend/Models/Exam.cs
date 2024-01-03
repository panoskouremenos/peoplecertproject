using System;
using System.Collections.Generic;

namespace PC_backend.Models;

public partial class Exam
{
    public int ExamId { get; set; }

    public int? CandidateId { get; set; }

    public string? TestCode { get; set; }

    public DateTime? DateTaken { get; set; }

    public int? TotalScore { get; set; }

    public int? PassingMark { get; set; }

    public virtual Candidate? Candidate { get; set; }

    public virtual ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
