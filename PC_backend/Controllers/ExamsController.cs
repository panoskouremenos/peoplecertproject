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
    public class ExamsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Exams
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

        // GET: api/Exams/5
        [HttpGet("{id}")]
        public IActionResult GetExam(int id)
        {
          if (_context.Exams == null)
          {
              return NotFound();
          }

          var exam =  _context.Exams.Include(c=>c.ExamResults).FirstOrDefault(c=>c.ExamId == id) ;

            if (exam == null)
            {
                return NotFound();
            }

            return Ok(exam);
        }

        // PUT: api/Exams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutExam(int id, ExamUpdatedto examUpdatedto)
        {
            
            var exam = _context.Exams.Include(c=>c.ExamResults).FirstOrDefault(c => c.ExamId == id);

            if(exam == null)
            {
                return NotFound();
            }

            //exam.CandidateId = examUpdatedto.CandidateId;
            //exam.CertificateId = examUpdatedto.CertificateId;
            //exam.DateAssigned = examUpdatedto.DateAssigned;

            // Clear existing ExamResults 
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

		// POST: api/Exams
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public IActionResult PostExam(Examdto examdto)
		{
			Exam exam = new Exam
			{
				CandidateId = examdto.CandidateId,
				VoucherId = examdto.VoucherId,
				DateAssigned = examdto.DateAssigned
			};

			_context.Exams.Add(exam);
			_context.SaveChanges();

			return Ok(exam);
		}

		// DELETE: api/Exams/5
		[HttpDelete("{id}")]
        public IActionResult DeleteExam(int id)
        {
            if (_context.Exams == null)
            {
                return NotFound();
            }
            var exam = _context.Exams.Include(c=>c.ExamResults).FirstOrDefault(c=>c.ExamId==id);
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
    }
}
