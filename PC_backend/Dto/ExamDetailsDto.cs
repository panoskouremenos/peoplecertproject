namespace PC_backend.Dto
{
    public class ExamDetailsDto
    {
        public int? ExamId { get; set; }
        public int? CandidateId { get; set; }
        public int CertificateId { get; set; }
        public string? CertificateTitle { get; set; }

        public ICollection<QuestionDto>? Questions { get; set; } = new List<QuestionDto>();
    }

    public class QuestionDto
    {
        public int? QuestionId { get; set; }
        public int? CertificateTopicMarksId { get; set; }
        public string? QuestionText { get; set; }
        public string? PossibleAnswers { get; set; }
    }
}

