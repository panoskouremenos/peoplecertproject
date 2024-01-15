using PC_backend.Models;

namespace PC_backend.Dto
{
    public class ExamResultdto
    {

        public int ExamId { get; set; }

        public int Score { get; set; }

        public DateTime ResultDate { get; set; }

        public bool Passed { get; set; }

       // public virtual Examdto? Exam { get; set; }


    }
}
