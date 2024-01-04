using System;
using System.Collections.Generic;

namespace PC_backend.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public int? CertificateTopicMarksId { get; set; }

    public string? QuestionText { get; set; }

    public string? QuestionType { get; set; }

    public string? PossibleAnswers { get; set; }

    public string? Answer { get; set; }

    public virtual CertificateTopicMark? CertificateTopicMarks { get; set; }
}
