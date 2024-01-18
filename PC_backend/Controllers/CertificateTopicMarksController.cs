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
	public class CertificateTopicMarksController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public CertificateTopicMarksController(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Gets all the topics along with the questions it contains.
		/// </summary>
		//TOPIC =GET= ACTIONS START
		[Authorize(Roles = "1")]
		[HttpGet]
		public IActionResult GetCertificateTopicMarks()
		{
			if (_context.CertificateTopicMarks == null)
			{
				return NotFound();
			}

			var certsTopikMarksWithQyestions = _context.CertificateTopicMarks.Include(c => c.Questions);
			return Ok(certsTopikMarksWithQyestions);
		}

		/// <summary>
		/// Gets the TopicMark with a certain id.
		/// </summary>
		[Authorize(Roles = "1")]
		[HttpGet("{id}")]
		public IActionResult GetCertificateTopicMark(int id)
		{
			if (_context.CertificateTopicMarks == null)
			{
				return NotFound();
			}
			var certificateTopicMark = _context.CertificateTopicMarks.Include(c => c.Questions).FirstOrDefault(c => c.CertificateTopicMarksId == id);

			if (certificateTopicMark == null)
			{
				return NotFound();
			}

			return Ok(certificateTopicMark);
		}

		[Authorize]
		[HttpGet("GetExamReport/{certificateId}")]
		public async Task<IActionResult> GetExamReport(int certificateId)
		{
			var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
			if (userIdClaim == null)
			{
				return Unauthorized("User ID is missing.");
			}

			var userId = int.Parse(userIdClaim.Value);
			var candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.UserId == userId);
			if (candidate == null)
			{
				return NotFound("Candidate not found.");
			}

			var topics = await _context.CertificateTopicMarks
				.Where(ctm => ctm.CertificateId == certificateId)
				.Select(ctm => new
				{
					TopicDesc = ctm.TopicDesc,
					Questions = ctm.Questions.Select(q => new
					{
						QuestionId = q.QuestionId,
						IsCorrect = q.ExamCandAnswers.Any(eca => eca.IsCorrect)
					})
				})
				.ToListAsync();

			var reportData = topics.Select(t => new
			{
				Topic = t.TopicDesc,
				MaxAwardedMarks = t.Questions.Count(),
				AwardedMarks = t.Questions.Count(q => q.IsCorrect)
			});

			// Code to generate PDF using reportData goes here
			Console.WriteLine(reportData);
			return Ok(reportData); // Or return the generated PDF file
		}

		//TOPIC =GET= ACTIONS END

		//TOPIC =PUT= ACTIONS START
		/// <summary>
		/// Edits a Topic Mark with a certain ID.
		/// </summary>
		[Authorize(Roles = "2")]
		[HttpPut("{id}")]
		public IActionResult PutCertificateTopicMark(int id, CertificateTopicMarkdto certificateTopicMarkdto)
		{
			var certificateTopicMark = _context.CertificateTopicMarks
				.Include(c => c.Questions)
				.FirstOrDefault(c => c.CertificateTopicMarksId == id);

			if (certificateTopicMark == null)
			{
				return NotFound();
			}

			certificateTopicMark.TopicDesc = certificateTopicMarkdto.TopicDesc;
			//certificateTopicMark.CertificateId = certificateTopicMarkdto.CertificateId;
			certificateTopicMark.Questions = certificateTopicMarkdto.Questions
			   .Select(questiondto => new Question
			   {
				   QuestionText = questiondto.QuestionText,
				   QuestionType = questiondto.QuestionType,
				   PossibleAnswers = questiondto.PossibleAnswers,
				   Answer = questiondto.Answer

			   }).ToList();

			_context.SaveChanges();
			return Ok(certificateTopicMark);

		}
		//TOPIC =PUT= ACTIONS END

		//TOPIC =POST= ACTIONS START
		/// <summary>
		/// Inserts into the database a TopicMark (Probably unusable).
		/// </summary>
		[Authorize(Roles = "2")]
		[HttpPost]
		public IActionResult PostCertificateTopicMark(CertificateTopicMarkdto certificateTopicMarkdto)
		{
			if (_context.CertificateTopicMarks == null)
			{
				return Problem("Entity is null.");
			}

			CertificateTopicMark certificateTopicMark = new CertificateTopicMark()
			{
				TopicDesc = certificateTopicMarkdto.TopicDesc,

			};

			certificateTopicMark.Questions = certificateTopicMarkdto.Questions
				.Select(questiondto => new Question
				{
					QuestionText = questiondto.QuestionText,
					QuestionType = questiondto.QuestionType,
					PossibleAnswers = questiondto.PossibleAnswers,
					Answer = questiondto.Answer

				}).ToList();


			_context.CertificateTopicMarks.Add(certificateTopicMark);
			_context.SaveChanges();

			return Ok(certificateTopicMark);
		}
		//TOPIC =DELETE= ACTIONS START
		/// <summary>
		/// Deletes a certain topicmark (I have to test if it also deletes the questions binded)
		/// </summary>
		[Authorize(Roles = "2")]
		[HttpDelete("{id}")]
		public IActionResult DeleteCertificateTopicMark(int id)
		{
			if (_context.CertificateTopicMarks == null)
			{
				return NotFound();
			}

			var certificateTopicMark = _context.CertificateTopicMarks
				.Include(c => c.Questions)
				.FirstOrDefault(c => c.CertificateTopicMarksId == id);
			if (certificateTopicMark == null)
			{
				return NotFound();
			}

			_context.Questions.RemoveRange(certificateTopicMark.Questions);
			_context.CertificateTopicMarks.Remove(certificateTopicMark);
			_context.SaveChanges();

			return Ok(certificateTopicMark);
		}

		private bool CertificateTopicMarkExists(int id)
		{
			return (_context.CertificateTopicMarks?.Any(e => e.CertificateTopicMarksId == id)).GetValueOrDefault();
		}
		//TOPIC =DELETE= ACTIONS END
	}
}
