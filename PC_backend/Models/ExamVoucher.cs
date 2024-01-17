using System;
using System.Collections.Generic;

namespace PC_backend.Models;

public partial class ExamVoucher
{
    public int VoucherId { get; set; }

    public int? ProductId { get; set; }

    public int? CandidateId { get; set; }

    public int? CertificateId { get; set; }

    public Guid VoucherCode { get; set; }

    public bool IsUsed { get; set; }

    public virtual Candidate? Candidate { get; set; }

    public virtual Certificate? Certificate { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual EshopProduct? Product { get; set; }
}
