using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PC_backend.Models;
using PC_backend.Services;

namespace PC_backend.Controllers
{ 

	[Route("api/[controller]")]
	[ApiController]
	public class CandidatesController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public CandidatesController(ApplicationDbContext context)
		{
			_context = context;
		}
		[Authorize(Roles = "1" )]
		[HttpGet("{id}")]
		public async Task<ActionResult<Candidate>> Get(int id)
		{
			var candidate = await _context.Candidates
			.Include(c => c.CandidateAddresses)
			.Include(c => c.CandidatePhotoIds)
			.FirstOrDefaultAsync(c => c.CandidateId == id);

			if (candidate == null)
			{
				return NotFound();
			}
			return candidate;
		}
		[Authorize(Roles = "1")]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Candidate>>> GetAll()
		{
			var candidates = await _context.Candidates
				.Include(c => c.CandidateAddresses)
				.Include(c => c.CandidatePhotoIds)
				.ToListAsync();

			if (!candidates.Any())
			{
				return NotFound();
			}

			return Ok(candidates);
		}

		[Authorize(Roles = "1")]
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CandidateCreateDto candidateCreateDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
			if (userIdClaim == null)
			{
				return Unauthorized("User ID is missing in the token.");
			}
			var userId = int.Parse(userIdClaim.Value);

			var candidate = new Candidate
			{
				FirstName = candidateCreateDto.FirstName,
				MiddleName = candidateCreateDto.MiddleName,
				LastName = candidateCreateDto.LastName,
				Gender = candidateCreateDto.Gender,
				BirthDate = candidateCreateDto.BirthDate,
				NativeLanguage = candidateCreateDto.NativeLanguage,
				LandlineNumber = candidateCreateDto.LandlineNumber,
				MobileNumber = candidateCreateDto.MobileNumber,
				UserId = userId
			};

			foreach (var addressDto in candidateCreateDto.Addresses)
			{
				var address = new CandidateAddress
				{
					Address = addressDto.Address,
					AddressLine2 = addressDto.AddressLine2,
					CountryOfResidence = addressDto.CountryOfResidence,
					StateTerritoryProvince = addressDto.StateTerritoryProvince,
					TownCity = addressDto.TownCity,
					PostalCode = addressDto.PostalCode,

					
				};

				candidate.CandidateAddresses.Add(address);
			}

			foreach (var PhotoIDdto in candidateCreateDto.PhotoIDs)
			{
				var photocreds = new CandidatePhotoId
				{
					PhotoIdtype = PhotoIDdto.PhotoIdtype,
					PhotoIdnumber = PhotoIDdto.PhotoIdnumber,
					PhotoIdissueDate = PhotoIDdto.PhotoIdissueDate,
				};

				candidate.CandidatePhotoIds.Add(photocreds);
			}

			_context.Candidates.Add(candidate);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(Get), new { id = candidate.CandidateId }, candidate);
		}
		[Authorize(Roles = "2")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var candidate = await _context.Candidates
				.Include(c => c.CandidateAddresses)
				.Include(c => c.CandidatePhotoIds)
				.FirstOrDefaultAsync(c => c.CandidateId == id);

			if (candidate == null)
			{
				return NotFound();
			}

			_context.CandidateAddresses.RemoveRange(candidate.CandidateAddresses);
			_context.CandidatePhotoIds.RemoveRange(candidate.CandidatePhotoIds);
			_context.Candidates.Remove(candidate);

			await _context.SaveChangesAsync();

			return NoContent();
		}

		[Authorize(Roles = "1")]
		[HttpPut]
		public async Task<IActionResult> Put([FromBody] CandidateUpdateDto candidateUpdateDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
			if (userIdClaim == null)
			{
				return Unauthorized("User ID is missing in the token.");
			}

			var userId = int.Parse(userIdClaim.Value);

			var candidate = await _context.Candidates
				.Include(c => c.CandidateAddresses)
				.Include(c => c.CandidatePhotoIds)
				.FirstOrDefaultAsync(c => c.UserId == userId);

			if (candidate == null)
			{
				return NotFound();
			}
			candidate.FirstName = candidateUpdateDto.FirstName;
			candidate.MiddleName = candidateUpdateDto.MiddleName;
			candidate.LastName = candidateUpdateDto.LastName;
			candidate.Gender = candidateUpdateDto.Gender;
			candidate.BirthDate = candidateUpdateDto.BirthDate;
			candidate.NativeLanguage = candidateUpdateDto.NativeLanguage;
			candidate.LandlineNumber = candidateUpdateDto.LandlineNumber;
			candidate.MobileNumber = candidateUpdateDto.MobileNumber;

			_context.CandidateAddresses.RemoveRange(candidate.CandidateAddresses);
			foreach (var addressDto in candidateUpdateDto.Addresses)
			{
				var address = new CandidateAddress
				{
					Address = addressDto.Address,
					AddressLine2 = addressDto.AddressLine2,
					CountryOfResidence = addressDto.CountryOfResidence,
					StateTerritoryProvince = addressDto.StateTerritoryProvince,
					TownCity = addressDto.TownCity,
					PostalCode = addressDto.PostalCode,
				};

				candidate.CandidateAddresses.Add(address);
			}

			_context.CandidatePhotoIds.RemoveRange(candidate.CandidatePhotoIds);

			foreach (var photoIdDto in candidateUpdateDto.PhotoIDs)
			{
				var photoId = new CandidatePhotoId
				{

					PhotoIdtype = photoIdDto.PhotoIdtype,
					PhotoIdnumber = photoIdDto.PhotoIdnumber,
					PhotoIdissueDate = photoIdDto.PhotoIdissueDate,
				};

				candidate.CandidatePhotoIds.Add(photoId);
			}

			await _context.SaveChangesAsync();

			return NoContent();

		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] CandidateUpdateDto candidateUpdateDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var candidate = await _context.Candidates
				.Include(c => c.CandidateAddresses)
				.Include(c => c.CandidatePhotoIds)
				.FirstOrDefaultAsync(c => c.CandidateId == id);

			if (candidate == null)
			{
				return NotFound();
			}
			candidate.FirstName = candidateUpdateDto.FirstName;
			candidate.MiddleName = candidateUpdateDto.MiddleName;
			candidate.LastName = candidateUpdateDto.LastName;
			candidate.Gender = candidateUpdateDto.Gender;
			candidate.BirthDate = candidateUpdateDto.BirthDate;
			candidate.NativeLanguage = candidateUpdateDto.NativeLanguage;
			candidate.LandlineNumber = candidateUpdateDto.LandlineNumber;
			candidate.MobileNumber = candidateUpdateDto.MobileNumber;

			_context.CandidateAddresses.RemoveRange(candidate.CandidateAddresses);
			foreach (var addressDto in candidateUpdateDto.Addresses)
			{
				var address = new CandidateAddress
				{
					Address = addressDto.Address,
					AddressLine2 = addressDto.AddressLine2,
					CountryOfResidence = addressDto.CountryOfResidence,
					StateTerritoryProvince = addressDto.StateTerritoryProvince,
					TownCity = addressDto.TownCity,
					PostalCode = addressDto.PostalCode,
				};

				candidate.CandidateAddresses.Add(address);
			}

			_context.CandidatePhotoIds.RemoveRange(candidate.CandidatePhotoIds);
			foreach (var photoIdDto in candidateUpdateDto.PhotoIDs)
			{
				var photoId = new CandidatePhotoId
				{
					PhotoIdtype = photoIdDto.PhotoIdtype,
					PhotoIdnumber = photoIdDto.PhotoIdnumber,
					PhotoIdissueDate = photoIdDto.PhotoIdissueDate,
				};
				candidate.CandidatePhotoIds.Add(photoId);
			}

			await _context.SaveChangesAsync();

			return NoContent();
		}


	}


}
