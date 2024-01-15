namespace PC_backend.Dto
{
	public class PassedCertificateDto
	{
		public string CertificateTitle { get; set; }
		public DateTime ExamDate { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Maxscore { get; set; }
		public int Score { get; set; }
		public int ScorePercentage { get; set; }

		public bool Passed { get; set; }


	}
}
