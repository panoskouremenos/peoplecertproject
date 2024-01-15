using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PC_backend.Dto;
using PC_backend.Models;
using PC_backend.Services;

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

		/// <summary>
		/// Gets all Certificates in the Database.
		/// </summary>
		[HttpGet]
        public IActionResult GetCertificates()
        {
          if (_context.Certificates == null)
          {
              return NotFound();
          }

            var certificatesWithTopicsAndQuestions = _context.Certificates
                  .Include(c => c.CertificateTopicMarks)
                  .ThenInclude(ctm=>ctm.Questions) ;
            return Ok(certificatesWithTopicsAndQuestions);
        }

		/// <summary>
		/// Get a certificate with a certain ID.
		/// </summary>
		[HttpGet("{id}")]
        public IActionResult GetCertificate(int id)
        {
          if (_context.Certificates == null)
          {
              return NotFound();
          }

            var certificate =  _context.Certificates
                .Include(c => c.CertificateTopicMarks)
                .ThenInclude(ctm => ctm.Questions)
                .FirstOrDefault(c=>c.CertificateId == id);  

            if (certificate == null)
            {
                return NotFound();
            }

            return Ok(certificate);
        }

		/// <summary>
		/// Edits a certificate with a certain ID.
		/// </summary>
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
        public IActionResult PutCertificate(int id, Certificatedto certificatedto)
        {
            var certificate = _context.Certificates
                .FirstOrDefault (c=>c.CertificateId == id);

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
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
            certificate.CertificateTopicMarks=certificatedto.CertificateTopicMarks
                .Select(certTopicMarks=> new CertificateTopicMark
                {
                    TopicDesc = certTopicMarks.TopicDesc,
                    Questions = certTopicMarks.Questions.Select(q => new Question
                    {
                        QuestionText = q.QuestionText,
                        QuestionType = q.QuestionType,
                        PossibleAnswers = q.PossibleAnswers,
                        Answer = q.Answer
                    }).ToList()
                }).ToList();

            _context.Certificates.Add(certificate);
            _context.SaveChanges();

            return Ok(certificate);
        }

		/// <summary>
		/// Deletes a certain certificate.
		/// </summary>
		[HttpDelete("{id}")]
        public  IActionResult DeleteCertificate(int id)
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

            // Remove Questions first
            var questionsToRemove = certificate.CertificateTopicMarks.SelectMany(ctm => ctm.Questions).ToList();
            _context.Questions.RemoveRange(questionsToRemove);

            // Remove CertificateTopicMarks
            _context.CertificateTopicMarks.RemoveRange(certificate.CertificateTopicMarks);

            // Remove Certificate
            _context.Certificates.Remove(certificate);

            _context.SaveChanges();

            return Ok(certificate);
        }

        private bool CertificateExists(int id)
        {
            return (_context.Certificates?.Any(e => e.CertificateId == id)).GetValueOrDefault();
        }
    }
}
