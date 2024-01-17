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
	public class CertificatesController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public CertificatesController(ApplicationDbContext context)
		{
			_context = context;
		}

		//CERTIFICATE =GET= ACTIONS START
		/// <summary>
		/// Gets all Certificates in the Database.
		/// </summary>
		
		[Authorize(Roles = "1")]
		[HttpGet]
		public IActionResult GetCertificates()
		{
			if (_context.Certificates == null)
			{
				return NotFound();
			}

			var certificatesWithTopicsAndQuestions = _context.Certificates
				  .Include(c => c.CertificateTopicMarks)
				  .ThenInclude(ctm => ctm.Questions);
			return Ok(certificatesWithTopicsAndQuestions);
		}

		/// <summary>
		/// Get a certificate with a certain ID.
		/// </summary>
		[Authorize(Roles = "1")]
		[HttpGet("{id}")]
		public IActionResult GetCertificate(int id)
		{
			if (_context.Certificates == null)
			{
				return NotFound();
			}

			var certificate = _context.Certificates
				.Include(c => c.CertificateTopicMarks)
				.ThenInclude(ctm => ctm.Questions)
				.FirstOrDefault(c => c.CertificateId == id);

			if (certificate == null)
			{
				return NotFound();
			}

			return Ok(certificate);
		}

		//Certificate =GET= Actions END

		//Certificate =PUT= Actions START
		/// <summary>
		/// Edits a certificate with a certain ID.
		/// </summary>

		[Authorize(Roles = "2")]
		[HttpPut("{id}")]
		public IActionResult PutCertificate(int id, Certificatedto certificatedto)
		{
			var certificate = _context.Certificates
				.FirstOrDefault(c => c.CertificateId == id);

			if (certificate == null)
			{
				return NotFound();
			}

			certificate.Title = certificatedto.Title;
			certificate.AssessmentTestCode = certificatedto.AssessmentTestCode;
			certificate.MaximumScore = certificatedto.MaximumScore;
			certificate.MinimumScore = certificatedto.MinimumScore;

			certificate.CertificateTopicMarks = certificatedto.CertificateTopicMarks
				.Select(certTopicMarks => new CertificateTopicMark
				{
					TopicDesc = certTopicMarks.TopicDesc,

				}).ToList();

			_context.SaveChanges();

			return Ok(certificate);
		}

		/// <summary>
		/// Creates a certificate.
		/// </summary>
		//Certificate =POST= Actions START

		[Authorize(Roles = "2")]
		[HttpPost]
		public IActionResult PostCertificate(Certificatedto certificatedto)
		{
			if (_context.Certificates == null)
			{
				return Problem("Entity is null.");
			}

			Certificate certificate = new Certificate()
			{
				Title = certificatedto.Title,
				AssessmentTestCode = certificatedto.AssessmentTestCode,
				MaximumScore = certificatedto.MaximumScore,
				MinimumScore = certificatedto.MinimumScore,
			};
			certificate.CertificateTopicMarks = certificatedto.CertificateTopicMarks
				.Select(certTopicMarks => new CertificateTopicMark
				{
					TopicDesc = certTopicMarks.TopicDesc,
					Questions = certTopicMarks.Questions.Select(q => new Question
					{
						QuestionText = q.QuestionText,
						QuestionType = q.QuestionType,
						PossibleAnswers = q.PossibleAnswers,
						//Answer = q.Answer
					}).ToList()
				}).ToList();

			_context.Certificates.Add(certificate);
			_context.SaveChanges();
			EshopProduct product = new EshopProduct
			{
				CertificateId = certificate.CertificateId,
                ProductName = certificate.Title,
                Description = certificate.Title,
                AvailableStock = 1,
                Price = 0 ,
                Deleted = true

			};


			_context.EshopProducts.Add(product);
			_context.SaveChanges();
			return Ok(certificate);
		}

		//Certificate =DELETE= Actions END
		/// <summary>
		/// Deletes a certain certificate.
		/// </summary>
		[Authorize(Roles = "2")]
		[HttpDelete("{id}")]
		public IActionResult DeleteCertificate(int id)
		{
			if (_context.Certificates == null)
			{
				return NotFound();
			}
			var certificate = _context.Certificates
				 .Include(c => c.CertificateTopicMarks)
				 .ThenInclude(ctm => ctm.Questions)
				 .FirstOrDefault(c => c.CertificateId == id);

			if (certificate == null)
			{
				return NotFound();
			}

			var questionsToRemove = certificate.CertificateTopicMarks.SelectMany(ctm => ctm.Questions).ToList();
			_context.Questions.RemoveRange(questionsToRemove);
			_context.CertificateTopicMarks.RemoveRange(certificate.CertificateTopicMarks);
			_context.Certificates.Remove(certificate);
			_context.SaveChanges();
			return Ok(certificate);
		}
		//Maybe not needed, will be omited in newer versions.
		private bool CertificateExists(int id)
		{
			return (_context.Certificates?.Any(e => e.CertificateId == id)).GetValueOrDefault();
		}
		//Certificate =DELETE= Actions END
	}
}
