using System;
using System.Collections.Generic;

namespace PC_backend.Models;

public partial class ExamResult
{
    public int ResultId { get; set; }

    public int? ExamId { get; set; }

    public int? CandidateId { get; set; }

    public int? Score { get; set; }

    public DateTime? ResultDate { get; set; }

    public int? Passed { get; set; }

    public int? CertificateId { get; set; }

    public virtual Candidate? Candidate { get; set; }

    public virtual Certificate? Certificate { get; set; }

    public virtual Exam? Exam { get; set; }
}
