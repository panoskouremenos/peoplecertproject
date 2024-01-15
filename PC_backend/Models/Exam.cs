using System;
using System.Collections.Generic;

namespace PC_backend.Models;

public partial class Exam
{
    public int ExamId { get; set; }

    public int? CandidateId { get; set; }

    public int? VoucherId { get; set; }

    public DateTime? DateAssigned { get; set; }

    public virtual Candidate? Candidate { get; set; }

    public virtual ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();

    public virtual ExamVoucher? Voucher { get; set; }
}
