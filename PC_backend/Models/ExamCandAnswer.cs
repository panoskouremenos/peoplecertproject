using System;
using System.Collections.Generic;

namespace PC_backend.Models;

public partial class ExamCandAnswer
{
    public int ExamCandAnswerId { get; set; }

    public int? ResultId { get; set; }

    public int? QuestionId { get; set; }

    public string? CandAnswer { get; set; }

    public bool? IsCorrect { get; set; }

    public virtual Question? Question { get; set; }

    public virtual ExamResult? Result { get; set; }
}
