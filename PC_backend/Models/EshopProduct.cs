using System;
using System.Collections.Generic;

namespace PC_backend.Models;

public partial class EshopProduct
{
    public int ProductId { get; set; }

    public int CertificateId { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? AvailableStock { get; set; }

    public bool? Deleted { get; set; }

    public virtual Certificate? Certificate { get; set; }

    public virtual ICollection<ExamVoucher> ExamVouchers { get; set; } = new List<ExamVoucher>();

    public virtual ICollection<UserCertificatePurchase> UserCertificatePurchases { get; set; } = new List<UserCertificatePurchase>();
}
