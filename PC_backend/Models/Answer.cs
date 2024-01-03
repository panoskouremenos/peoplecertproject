using System;
using System.Collections.Generic;

namespace PC_backend.Models;

public partial class Answer
{
    public int AnswerId { get; set; }

    public int? QuestionId { get; set; }

    public string? AnswerText { get; set; }

    public int? IsCorrect { get; set; }

    public virtual Question? Question { get; set; }
}
