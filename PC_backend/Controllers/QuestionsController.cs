using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
		//QUESTIONS =GET= ACTIONS START
		/// <summary>
		/// Gets all the questions in the database.
		/// </summary>
		[Authorize(Roles = "1")]
		[HttpGet]
        public IActionResult GetQuestions()
        {
          if (_context.Questions == null)
          {
              return NotFound();
          }
            return  Ok(_context.Questions);
        }

		/// <summary>
		/// Gets a question by the id assigned.
		/// </summary>
		[Authorize(Roles = "1")]
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
		//QUESTIONS =GET= ACTIONS END

		//QUESTIONS =PUT= ACTIONS START
		/// <summary>
		/// Edit a question of a certain ID
		/// </summary>
		/// 
		[Authorize(Roles = "2")]
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

		//QUESTIONS =PUT= ACTIONS END

		//QUESTIONS =POST= ACTIONS START
		/// <summary>
		/// Writes a question to the database (You have to assign the topic number).
		/// </summary>
		/// 
		[Authorize(Roles = "2")]
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
		//QUESTIONS =POST= ACTIONS END

		//QUESTIONS =DELETE= ACTIONS START
		/// <summary>
		/// Deletes a certain question from the table by passing the id.
		/// </summary>
		/// 
		[Authorize(Roles = "2")]
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
		//QUESTIONS =DELETE= ACTIONS END
	}
}
