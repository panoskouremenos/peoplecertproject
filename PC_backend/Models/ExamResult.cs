using System;
using System.Collections.Generic;

namespace PC_backend.Models;

public partial class ExamResult
{
    public int ResultId { get; set; }

    public int? ExamId { get; set; }

    public int Score { get; set; }

    public DateTime ResultDate { get; set; }

    public bool Passed { get; set; }

    public virtual Exam? Exam { get; set; }

    public virtual ICollection<ExamCandAnswer> ExamCandAnswers { get; set; } = new List<ExamCandAnswer>();
}
