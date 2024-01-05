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
    public class CertificateTopicMarksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CertificateTopicMarksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CertificateTopicMarks
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

        // GET: api/CertificateTopicMarks/5
        [HttpGet("{id}")]
        public IActionResult GetCertificateTopicMark(int id)
        {
          if (_context.CertificateTopicMarks == null)
          {
              return NotFound();
          }
            var certificateTopicMark = _context.CertificateTopicMarks.Include(c=>c.Questions).FirstOrDefault(c=>c.CertificateTopicMarksId==id);

            if (certificateTopicMark == null)
            {
                return NotFound();
            }

            return Ok(certificateTopicMark);
        }

        // PUT: api/CertificateTopicMarks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutCertificateTopicMark(int id, CertificateTopicMarkdto certificateTopicMarkdto)
        {
            var certificateTopicMark = _context.CertificateTopicMarks
                .Include(c => c.Questions)
                .FirstOrDefault(c => c.CertificateTopicMarksId == id);

            if(certificateTopicMark == null)
            {
                return NotFound();
            }

            certificateTopicMark.TopicDesc = certificateTopicMarkdto.TopicDesc;
            certificateTopicMark.NumberOfAwardedMarks = certificateTopicMarkdto.NumberOfAwardedMarks;
            certificateTopicMark.NumberOfPossibleMarks = certificateTopicMarkdto.NumberOfPossibleMarks;
            //certificateTopicMark.CertificateId = certificateTopicMarkdto.CertificateId;
            //prosthesa auto gia na ginetai update kai tis qyestion sto questions
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

        // POST: api/CertificateTopicMarks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
                NumberOfAwardedMarks = certificateTopicMarkdto.NumberOfAwardedMarks,
                NumberOfPossibleMarks = certificateTopicMarkdto.NumberOfPossibleMarks
                
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

        // DELETE: api/CertificateTopicMarks/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCertificateTopicMark(int id)
        {
            if (_context.CertificateTopicMarks == null)
            {
                return NotFound();
            }

            var certificateTopicMark = _context.CertificateTopicMarks
                .Include(c=>c.Questions)
                .FirstOrDefault(c=>c.CertificateTopicMarksId == id);
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
    }
}
