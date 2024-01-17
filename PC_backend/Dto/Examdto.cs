using PC_backend.Models;

namespace PC_backend.Dto
{
    public class Examdto
    {
        public int? CandidateId { get; set; }

        public DateTime DateAssigned { get; set; }

        public int? CertificateId { get; set; }

        public int? VoucherId { get; set; }

		// public virtual Candidate? Candidate { get; set; }

		// public virtual Certificate? Certificate { get; set; }

		public virtual ICollection<ExamResultdto>? ExamResults { get; set; } = new List<ExamResultdto>();
	}

	public class PurchaseAndExamDto
	{
		public int ProductId { get; set; }
		public DateTime PurchaseDate { get; set; }
	}

}
