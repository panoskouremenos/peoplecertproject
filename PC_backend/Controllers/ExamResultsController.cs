using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PC_backend.Dto;
using PC_backend.Models;
using PC_backend.Services;
using Microsoft.AspNetCore.Authorization;
using PC_backend.Utilities;

namespace PC_backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExamResultsController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public ExamResultsController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetExamResults()
		{
			if (_context.ExamResults == null)
			{
				return NotFound();
			}

			var resultDtos = _context.ExamResults
		 .Select(er => new
		 {
			 resultId = er.ResultId,
			 examId = er.ExamId,
			 score = er.Score,
			 resultDate = er.ResultDate,
			 passed = er.Passed,
			 //exam = er.Exam, // Include other properties if needed
			 // examCandAnswers = er.ExamCandAnswers // Include other properties if needed
		 })
		 .ToList();

			return Ok(resultDtos);
			//  return Ok(_context.ExamResults);
		}

		// GET: api/ExamResults/5
		[HttpGet("{id}")]
		public IActionResult GetExamResult(int id)
		{
			if (_context.ExamResults == null)
			{
				return NotFound();
			}
			var examResult = _context.ExamResults
				.Include(e => e.Exam)
				.Include(eca => eca.ExamCandAnswers)
				.FirstOrDefault(c => c.ResultId == id);

			if (examResult == null)
			{
				return NotFound();
			}

			return Ok(examResult);
			// return Ok(examResultDto);
		}

		[Authorize]
		[HttpGet("UserCertificates")]
		public async Task<IActionResult> UserCertificates()
		{
			var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
			if (userIdClaim == null)
			{
				return Unauthorized(new { message = "User ID is missing in the token." });
			}

			var userId = int.Parse(userIdClaim.Value);

			// Query the database to find the candidate associated with this user ID
			var candidate = await _context.Candidates
				.FirstOrDefaultAsync(c => c.UserId == userId);

			if (candidate == null)
			{
				return Ok(new { message = "User is not a candidate" });
			}

			int CandId = candidate.CandidateId;

			var passedCertificates = _context.ExamResults
				.Include(er => er.Exam)
				.ThenInclude(exam => exam.Voucher)
				.ThenInclude(voucher => voucher.Certificate)
				.Where(er => er.Exam.CandidateId == candidate.CandidateId && er.Passed == true)
				.Select(er => new
				{
					CertificateTitle = er.Exam.Voucher.Certificate.Title,
					ExamDate = er.ResultDate,
					FirstName = candidate.FirstName,
					LastName = candidate.LastName,
					Score = er.Score,
					MaxScore = er.Exam.Voucher.Certificate.MaximumScore
				})
				.ToList()
				.Select(pc => new PassedCertificateDto
				{
					CertificateTitle = pc.CertificateTitle,
					ExamDate = (DateTime)pc.ExamDate,
					FirstName = pc.FirstName,
					LastName = pc.LastName,
					ScorePercentage = ScoreUtility.ConvertToPercentage(pc.Score, pc.MaxScore)
				})
				.Distinct()
				.ToList();

			return Ok(passedCertificates);
		}

		[Authorize]
		[HttpGet("MyExamResults")]
		public async Task<IActionResult> MyExamResults()
		{
			var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
			if (userIdClaim == null)
			{
				return Unauthorized(new { message = "User ID is missing in the token." });
			}

			var userId = int.Parse(userIdClaim.Value);

			// Query the database to find the candidate associated with this user ID
			var candidate = await _context.Candidates
				.FirstOrDefaultAsync(c => c.UserId == userId);

			if (candidate == null)
			{
				return Ok(new { message = "User is not a candidate" });
			}

			int CandId = candidate.CandidateId;

			var passedCertificates = _context.ExamResults
				.Include(er => er.Exam)
				.Include(er => er.Exam)
				.ThenInclude(exam => exam.Voucher)
				.ThenInclude(voucher => voucher.Certificate)
				.Where(er => er.Exam.CandidateId == candidate.CandidateId)
				.Select(er => new
				{
					CertificateTitle = er.Exam.Voucher.Certificate.Title,
					ExamDate = er.ResultDate,
					FirstName = candidate.FirstName,
					LastName = candidate.LastName,
					Score = er.Score,
					Passed = er.Passed,
					MaxScore = er.Exam.Voucher.Certificate.MaximumScore
				})
				.ToList()
				.Select(pc => new PassedCertificateDto
				{
					CertificateTitle = pc.CertificateTitle,
					ExamDate = (DateTime)pc.ExamDate,
					FirstName = pc.FirstName,
					LastName = pc.LastName,
					Passed = (bool)pc.Passed,
					ScorePercentage = ScoreUtility.ConvertToPercentage(pc.Score, pc.MaxScore)
				})
				.Distinct()
				.ToList();
			return Ok(passedCertificates);
		}


		// PUT: api/ExamResults/5
		//[HttpPut("{id}")]
		//      public IActionResult PutExamResult(int id, ExamResultdto examResultdto)
		//      {
		//          var examResult =_context.ExamResults.FirstOrDefault(c=>c.ResultId == id);

		//          if (examResult == null)
		//          {
		//              return NotFound();
		//          }

		//          examResult.ExamId = examResultdto.ExamId;
		//          examResult.Score = examResultdto.Score;
		//          examResult.ResultDate = examResultdto.ResultDate;
		//          examResult.Passed = examResultdto.Passed;

		//          _context.SaveChanges();
		//          return Ok(examResult);


		//      }

		// POST: api/ExamResults
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public IActionResult PostExamResult(ExamResultdto examResultdto)
		{
			if (_context.ExamResults == null)
			{
				return Problem("Entity is null.");
			}

			ExamResult examResult = new ExamResult()
			{
				ExamId = examResultdto.ExamId,
				Score = (int)examResultdto.Score,
				ResultDate = (DateTime)examResultdto.ResultDate,
				Passed = examResultdto.Passed
			};



			_context.ExamResults.Add(examResult);
			_context.SaveChanges();

			return Ok(examResult);
		}

		// DELETE: api/ExamResults/5
		[HttpDelete("{id}")]
		public IActionResult DeleteExamResult(int id)
		{
			if (_context.ExamResults == null)
			{
				return NotFound();
			}
			var examResult = _context.ExamResults.FirstOrDefault(c => c.ResultId == id);

			if (examResult == null)
			{
				return NotFound();
			}

			_context.ExamResults.Remove(examResult);
			_context.SaveChanges();

			return Ok(examResult);
		}

		private bool ExamResultExists(int id)
		{
			return (_context.ExamResults?.Any(e => e.ResultId == id)).GetValueOrDefault();
		}

		[HttpGet("{examId}/Results")]
		public IActionResult GetExamResults(int examId)
		{
			var examResult = _context.ExamResults
				.Where(er => er.ExamId == examId)
				.FirstOrDefault();

			if (examResult == null)
			{
				return NotFound("Exam result not found");
			}

			var passOrFail = (examResult.Score >= 3) ? "Pass" : "Fail";


			examResult.Passed = (examResult.Score >= 3);
			_context.SaveChanges();
			/*if (passOrFail == "Pass")
            {
                examResult.Passed = true;
            }
            else
            {
                examResult.Passed = false;
            }
            _context.SaveChanges();*/

			var resultDto = new
			{
				ExamId = examResult.ExamId,
				Score = examResult.Score,
				PassOrFail = passOrFail

			};

			return Ok(resultDto);
		}
	}
}
