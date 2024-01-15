using System;
using System.Collections.Generic;

namespace PC_backend.Models;

public partial class Certificate
{
    public int CertificateId { get; set; }

    public string Title { get; set; }

    public string? AssessmentTestCode { get; set; }

    public int MinimumScore { get; set; }

    public int MaximumScore { get; set; }

    public bool? Deleted { get; set; }

    public virtual ICollection<CertificateTopicMark> CertificateTopicMarks { get; set; } = new List<CertificateTopicMark>();

    public virtual ICollection<EshopProduct> EshopProducts { get; set; } = new List<EshopProduct>();

    public virtual ICollection<ExamVoucher> ExamVouchers { get; set; } = new List<ExamVoucher>();
}
