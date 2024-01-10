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
    public class ExamResultsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExamResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ExamResults
        [HttpGet]
        public IActionResult GetExamResults()
        {
          if (_context.ExamResults == null)
          {
              return NotFound();
          }

            return Ok(_context.ExamResults);
        }

        // GET: api/ExamResults/5
        [HttpGet("{id}")]
        public IActionResult GetExamResult(int id)
        {
          if (_context.ExamResults == null)
          {
              return NotFound();
          }
            var examResult =_context.ExamResults
                .Include(e=>e.Exam)
                .FirstOrDefault(c=>c.ResultId == id);

            if (examResult == null)
            {
                return NotFound();
            }

            return Ok(examResult);
           // return Ok(examResultDto);
        }

        // PUT: api/ExamResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutExamResult(int id, ExamResultdto examResultdto)
        {
            var examResult =_context.ExamResults.FirstOrDefault(c=>c.ResultId == id);
            
            if (examResult == null)
            {
                return NotFound();
            }

            examResult.ExamId = examResultdto.ExamId;
            examResult.Score = examResultdto.Score;
            examResult.ResultDate = examResultdto.ResultDate;
            examResult.Passed = examResultdto.Passed;

            _context.SaveChanges();
            return Ok(examResult);


        }

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
                Score = examResultdto.Score,
                ResultDate = examResultdto.ResultDate,
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
            var examResult = _context.ExamResults.FirstOrDefault(c=>c.ResultId == id);
            
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
    }
}
