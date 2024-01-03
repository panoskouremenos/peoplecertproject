using System;
using System.Collections.Generic;

namespace PC_backend.Models;

public partial class ExamVoucher
{
    public int VoucherId { get; set; }

    public int? ProductId { get; set; }

    public int? CandidateId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public DateTime? ExamDate { get; set; }

    public int? IsUsed { get; set; }

    public virtual Candidate? Candidate { get; set; }

    public virtual EshopProduct? Product { get; set; }
}
