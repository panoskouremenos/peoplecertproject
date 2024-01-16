using PC_backend.Models;

namespace PC_backend.Dto
{
    public class ExamVoucherDto
    {
       

        // public int VoucherId { get; set; }

        public int? ProductId { get; set; }

        public int? CandidateId { get; set; }

        public int? CertificateId { get; set; }

       // public DateTime? PurchaseDate { get; set; }

        public Guid? VoucherCode { get; set; }

      //  public DateTime? ExamDate { get; set; }

        public bool? IsUsed { get; set; }

        // public virtual Candidate? Candidate { get; set; }

        // public virtual Certificate? Certificate { get; set; }

        // public virtual EshopProduct? Product { get; set; }

        public virtual ICollection<Examdto> Exams { get; set; } = new List<Examdto>();

        
    }
}
