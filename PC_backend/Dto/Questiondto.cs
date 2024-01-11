using PC_backend.Models;

namespace PC_backend.Dto
{
    public class Questiondto
    {
        //public int QuestionId { get; set; }

        public int? CertificateTopicMarksId { get; set; }

        public string? QuestionText { get; set; }

        public int? QuestionType { get; set; }

        public string? PossibleAnswers { get; set; }

        public string? Answer { get; set; }
        
        //public virtual CertificateTopicMark? CertificateTopicMarks { get; set; }
    
}
}
