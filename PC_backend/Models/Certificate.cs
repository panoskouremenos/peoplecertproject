using System;
using System.Collections.Generic;

namespace PC_backend.Models;

public partial class Certificate
{
    public int CertificateId { get; set; }

    public string? Title { get; set; }

    public string? AssessmentTestCode { get; set; }

    public int? MinimumScore { get; set; }

    public int? MaximumScore { get; set; }

    public virtual ICollection<CertificateTopicMark> CertificateTopicMarks { get; set; } = new List<CertificateTopicMark>();

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
