using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dbmanip.Models;
using dbmanip.Services;

namespace dbmanip.Controllers{
[Route("api/[controller]")]
[ApiController]
public class CandidatesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CandidatesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Candidates
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Candidates>>> GetCandidates()
    {
        return await _context.tbl_Candidates.ToListAsync();
    }

    // GET: api/Candidates/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Candidates>> GetCandidates(int id)
    {
        var candidates = await _context.tbl_Candidates.FindAsync(id);

        if (candidates == null)
        {
            return NotFound();
        }

        return candidates;
    }

    // PUT: api/Candidates/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCandidates(int id, Candidates candidates)
    {
        if (id != candidates.CandidateID)
        {
            return BadRequest();
        }

        _context.Entry(candidates).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CandidatesExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Candidates
    [HttpPost]
    public async Task<ActionResult<Candidates>> PostCandidates(Candidates candidates)
    {
        _context.tbl_Candidates.Add(candidates);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCandidates", new { id = candidates.CandidateID }, candidates);
    }

    // DELETE: api/Candidates/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCandidates(int id)
    {
        var candidates = await _context.tbl_Candidates.FindAsync(id);
        if (candidates == null)
        {
            return NotFound();
        }

        _context.tbl_Candidates.Remove(candidates);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CandidatesExists(int id)
    {
        return _context.tbl_Candidates.Any(e => e.CandidateID == id);
    }
}
}