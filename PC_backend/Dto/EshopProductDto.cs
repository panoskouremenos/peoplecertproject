using PC_backend.Models;

namespace PC_backend.Dto
{
    public class EshopProductDto
    {
        // public int ProductId { get; set; }

        public string? ProductName { get; set; }
        public int CertificateId { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? AvailableStock { get; set; }

        public bool? Deleted { get; set; }

        // public virtual ICollection<ExamVoucher> ExamVouchers { get; set; } = new List<ExamVoucher>();
    }
}
