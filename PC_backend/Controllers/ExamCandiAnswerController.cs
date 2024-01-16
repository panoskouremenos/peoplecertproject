using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PC_backend.Dto;
using PC_backend.Services;

namespace PC_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamCandiAnswerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExamCandiAnswerController(ApplicationDbContext context)
        {
            _context = context;
        }



        [HttpPost("ProcessAnswers")]
        public IActionResult ProcessCandidateAnswers(List<ExamCandidateAnswerDto> candidateAnswers)
        {
            // Assuming candidateAnswers is a list of answers sent from the front end

            foreach (var answer in candidateAnswers)
            {
                // Retrieve the correct answer from the database
                var correctAnswer = _context.Questions
                    .Where(q => q.QuestionId == answer.QuestionId)
                    .Select(q => q.Answer)
                    .FirstOrDefault();

                // Check if the candidate's answer is correct
                bool isCorrect = (answer.CandAnswer == correctAnswer);

                // Update the ExamResult with the correctness information
                var examResult = _context.ExamResults
                    .FirstOrDefault(er => er.ExamId == answer.Result.ExamId);

                if (examResult != null)
                {
                    // Update the score based on correctness
                    if (isCorrect)
                    {
                        examResult.Score += 1;
                    }
                }

                // Save changes to the database
                _context.SaveChanges();
            }


            // Return the updated exam results
            var updatedExamResults = _context.ExamResults
                .Where(er => er.ExamId == candidateAnswers.FirstOrDefault().Result.ExamId)
                .Select(er => new ExamResultdto
                {
                    ExamId = er.ExamId,
                    Score = er.Score,
                    ResultDate = er.ResultDate,
                    Passed = (er.Score >= 3)
                })
                .ToList();

            return Ok(updatedExamResults);
        }

    }
}
