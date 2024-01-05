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
    public class QuestionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Questions
        [HttpGet]
        public IActionResult GetQuestions()
        {
          if (_context.Questions == null)
          {
              return NotFound();
          }
            return  Ok(_context.Questions);
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public IActionResult GetQuestion(int id)
        {
          if (_context.Questions == null)
          {
              return NotFound();
          }

          var question =  _context.Questions.FirstOrDefault(c=>c.QuestionId == id);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutQuestion(int id, Questiondto questiondto)
        {
            var question = _context.Questions.SingleOrDefault(c=>c.QuestionId==id);
            if (question == null)
            {
                return NotFound();
            }
            question.CertificateTopicMarksId = questiondto.CertificateTopicMarksId;
            question.QuestionType = questiondto.QuestionType;
            question.QuestionText = questiondto.QuestionText;
            question.PossibleAnswers = questiondto.PossibleAnswers;
            question.Answer = questiondto.Answer;

            _context.SaveChanges();
            return Ok(question);

        }

        // POST: api/Questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostQuestion(Questiondto questiondto)
        {
          if (_context.Questions == null)
          {
              return Problem("Entity is null.");
          }

            Question question = new Question()
            {

                //sto json edw an den valeis certtopId h to valeis null einai ok vazei null
                CertificateTopicMarksId = questiondto.CertificateTopicMarksId, 
                QuestionText = questiondto.QuestionText,
                QuestionType = questiondto.QuestionType,
                PossibleAnswers = questiondto.PossibleAnswers,
                Answer = questiondto.Answer
            };
            _context.Questions.Add(question);
            _context.SaveChanges();

            return Ok(question);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public IActionResult DeleteQuestion(int id)
        {
            if (_context.Questions == null)
            {
                return NotFound();
            }

            var question = _context.Questions.FirstOrDefault(c => c.QuestionId == id);

            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            _context.SaveChanges();

            return Ok(question);
        }

        private bool QuestionExists(int id)
        {
            return (_context.Questions?.Any(e => e.QuestionId == id)).GetValueOrDefault();
        }
    }
}
