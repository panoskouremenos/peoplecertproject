using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PC_backend.Dto;
using PC_backend.Models;
using PC_backend.Services;
using Microsoft.AspNetCore.Authorization;

namespace PC_backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExamsController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public ExamsController(ApplicationDbContext context)
		{
			_context = context;
		}

		//Exams(Pending) =GET= ACTIONS START
		[Authorize(Roles = "1")]
		[HttpGet]
		public IActionResult GetExams()
		{
			if (_context.Exams == null)
			{
				return NotFound();
			}
			var exam = _context.Exams.Include(c => c.ExamResults);

			return Ok(exam);
		}
		[Authorize(Roles = "1")]
		[HttpGet("{id}")]
		public IActionResult GetExam(int id)
		{
			if (_context.Exams == null)
			{
				return NotFound();
			}

			var exam = _context.Exams.Include(c => c.ExamResults).FirstOrDefault(c => c.ExamId == id);

			if (exam == null)
			{
				return NotFound();
			}

			return Ok(exam);
		}
		[Authorize(Roles = "1")]
		[HttpGet("{id}/Questions")]     //====================auro einai psiloKalo apo Katw
		public IActionResult GetExamQuestions(int id)
		{
			var examQuestions = _context.Exams
				.Include(c => c.Certificate)
					.ThenInclude(c => c.CertificateTopicMarks)
						.ThenInclude(c => c.Questions)
				.FirstOrDefault(c => c.ExamId == id);

			if (examQuestions != null)
			{
				var examDetailsDto = new ExamDetailsDto
				{
					ExamId = examQuestions.ExamId,
					CandidateId = examQuestions.CandidateId,
					CertificateId = examQuestions.CertificateId,
					CertificateTitle = examQuestions.Certificate.Title,
					Questions = examQuestions.Certificate.CertificateTopicMarks
						.SelectMany(ctm => ctm.Questions
							.Select(q => new QuestionDto
							{
								QuestionId = q.QuestionId,
								CertificateTopicMarksId = ctm.CertificateTopicMarksId,
								QuestionText = q.QuestionText,
								PossibleAnswers = q.PossibleAnswers
							}))
						.ToList()
				};

				return Ok(examQuestions);
			}

			return NotFound();
		}
		[Authorize(Roles = "1")]
		[HttpGet("{examId}/Results")]
		public IActionResult GetExamResults(int examId)
		{
			var examResults = _context.ExamResults
				.Where(er => er.ExamId == examId)
				.Select(er => new ExamResultdto
				{
					ExamId = er.ExamId,
					Score = er.Score,
					ResultDate = er.ResultDate,
					Passed = er.Passed
				})
				.ToList();

			return Ok(examResults);
		}
		//Exams(Pending) =GET= ACTIONS END

		//Exams(Pending) =PUT= ACTIONS START
		[Authorize(Roles = "1")]
		[HttpPut("{id}")]
		public IActionResult PutExam(int id, ExamUpdatedto examUpdatedto)
		{

			var exam = _context.Exams.Include(c => c.ExamResults).FirstOrDefault(c => c.ExamId == id);

			if (exam == null)
			{
				return NotFound();
			}

			exam.CandidateId = examUpdatedto.CandidateId;
			exam.CertificateId = examUpdatedto.CertificateId;
			exam.DateAssigned = examUpdatedto.DateAssigned;

			// Clearing Exams, Not needed
			// exam.ExamResults.Clear();
			/* exam.ExamResults = examdto.ExamResults
                 .Select(exRe => new ExamResult 
                 {
                     Score = exRe.Score,
                     ResultDate = exRe.ResultDate,
                     Passed = exRe.Passed
                 }).ToList();*/

			_context.SaveChanges();
			return Ok(exam);


		}
		//Exams(Pending) =PUT= ACTIONS END

		//Exams(Pending) =POST= ACTIONS START
		[Authorize(Roles = "1")]
		[HttpPost]
		public IActionResult PostExam(Examdto examdto)
		{
			Exam exam = new Exam
			{
				CandidateId = examdto.CandidateId,
				VoucherId = examdto.VoucherId,
				CertificateId = examdto.CertificateId,
				DateAssigned = examdto.DateAssigned
			};

			_context.Exams.Add(exam);
			_context.SaveChanges();

			ExamResult examResult = new ExamResult()
			{
				ExamId = exam.ExamId,
				Score = 0,
				ResultDate = DateTime.Now

			};

			_context.ExamResults.Add(examResult);
			_context.SaveChanges();


			return Ok(exam);
		}
		//Exams(Pending) =POST= ACTIONS END

		//Exams(Pending) =DELETE= ACTIONS START
		[Authorize(Roles = "1")]
		[HttpDelete("{id}")]
		public IActionResult DeleteExam(int id)
		{
			if (_context.Exams == null)
			{
				return NotFound();
			}
			var exam = _context.Exams.Include(c => c.ExamResults).FirstOrDefault(c => c.ExamId == id);
			if (exam == null)
			{
				return NotFound();
			}

			_context.ExamResults.RemoveRange(exam.ExamResults);
			_context.Exams.Remove(exam);
			_context.SaveChanges();

			return Ok(exam);
		}

		private bool ExamExists(int id)
		{
			return (_context.Exams?.Any(e => e.ExamId == id)).GetValueOrDefault();
		}
		//Exams(Pending) =DELETE= ACTIONS END




	}
}
