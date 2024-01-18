namespace PC_backend.Dto
{
    public class ExamCandidateAnswerDto
    {
        // public int ExamCandAnswerId { get; set; }

        // public int? ResultId { get; set; }

        public int? QuestionId { get; set; }

        public string? CandAnswer { get; set; }

        public bool IsCorrect { get; set; }

        //   public virtual Questiondto Question { get; set; }

        public virtual ExamResultdto? Result { get; set; }
    }
}
