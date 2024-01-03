using System;
using System.Collections.Generic;

namespace PC_backend.Models;

public partial class CertificateTopicMark
{
    public int CertificateTopicMarksId { get; set; }

    public int? CertificateId { get; set; }

    public string? TopicDesc { get; set; }

    public int? NumberOfAwardedMarks { get; set; }

    public int? NumberOfPossibleMarks { get; set; }

    public virtual Certificate? Certificate { get; set; }
}
