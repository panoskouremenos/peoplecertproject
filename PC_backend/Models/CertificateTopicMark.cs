using System;
using System.Collections.Generic;

namespace PC_backend.Models;

public partial class CertificateTopicMark
{
    public int CertificateTopicMarksId { get; set; }

    public int? CertificateId { get; set; }

    public string? TopicDesc { get; set; }

    public virtual Certificate? Certificate { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
