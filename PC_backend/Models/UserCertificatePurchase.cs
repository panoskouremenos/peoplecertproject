using System;
using System.Collections.Generic;

namespace PC_backend.Models;

public partial class UserCertificatePurchase
{
    public int PurchaseId { get; set; }

    public int? CandidateId { get; set; }

    public int? ProductId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public virtual Candidate? Candidate { get; set; }

    public virtual EshopProduct? Product { get; set; }
}
