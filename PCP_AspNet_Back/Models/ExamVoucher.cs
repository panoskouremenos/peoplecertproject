
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbmanip.Models{

public class ExamVoucher
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int VoucherID { get; set; }
    public int ProductID { get; set; }
    public int CandidateID { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime ExamDate { get; set; }
    public int IsUsed { get; set; }

    [ForeignKey("ProductID")]
    public EshopProduct Product { get; set; }

    [ForeignKey("CandidateID")]
    public Candidates Candidate { get; set; }
}
}