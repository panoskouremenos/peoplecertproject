using Microsoft.AspNetCore.Mvc;
using PC_backend.Dto;
using PC_backend.Services;
using Microsoft.AspNetCore.Authorization;

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

		//CAND.ANSWERS =POST= ACTIONS START
		[Authorize(Roles = "1")]
		[HttpPost("ProcessAnswers")]
		public IActionResult ProcessCandidateAnswers(List<ExamCandidateAnswerDto> candidateAnswers)
		{

			foreach (var answer in candidateAnswers)
			{
				var correctAnswer = _context.Questions
					.Where(q => q.QuestionId == answer.QuestionId)
					.Select(q => q.Answer)
					.FirstOrDefault();

				bool isCorrect = (answer.CandAnswer == correctAnswer);
                var examResult = _context.ExamResults.FirstOrDefault(er => er.ExamId == answer.Result.ExamId);
                Console.WriteLine(examResult);

				if (examResult != null)
				{
					if (isCorrect)
					{
						examResult.Score += 1;
					}
				}

				_context.SaveChanges();
			}

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
		//CAND.ANSWERS =POST= ACTIONS END

	}
}
