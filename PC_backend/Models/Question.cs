using System;
using System.Collections.Generic;

namespace PC_backend.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public int? ExamId { get; set; }

    public string? QuestionText { get; set; }

    public string? QuestionType { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Exam? Exam { get; set; }
}
