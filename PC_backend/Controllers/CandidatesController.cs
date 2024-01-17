using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PC_backend.Models;
using PC_backend.Services;
using PC_backend.Dto;
using Newtonsoft.Json;

//All the controllers that control the CRUD of Candidates.
//Auth tip: Role 1 = User
//          Role 2 = Admin
//          Role 3 = Quality Control //Probably not needed.
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

		//CANDIDATES GET ACTIONS START
		/// <summary>
		/// Gets the infos of a certain candidate, (for Admin use)(DEPRECATED)
		/// </summary>
		[Authorize]
		[HttpGet("MyCandidateInfo")]
		public async Task<IActionResult> GetMyCandidateInfo()
		{
			var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
			if (userIdClaim == null)
			{
				return Unauthorized(new { message = "User ID is missing in the token." });
			}

			var userId = int.Parse(userIdClaim.Value);

			var candidate = await _context.Candidates
				.Include(c => c.CandidateAddresses)
				.Include(c => c.CandidatePhotoIds)
				.FirstOrDefaultAsync(c => c.UserId == userId);

			if (candidate == null)
			{
				return Ok(new { message = "User is not a candidate!" });
			}
			return Ok(candidate);
		}

		/// <summary>
		/// Gets the infos of a certain candidate, (for Admin use)(DEPRECATED)
		/// </summary>
		[Authorize(Roles = "2")]
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

		/// <summary>
		/// Gets the infos of all candidates in existence. (for Admin use)
		/// </summary>
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
		//CANDIDATES GET ACTIONS END
		/// <summary>
		/// Makes the registered user a Candidate. Joins CandidatePhotoID and CandidateAddress together, accepts a json for all three of them together. Also binds his user profile with his candidate profile.
		/// </summary>
		//CANDIDATES POST ACTIONS START
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
				Email = candidateCreateDto.Email,
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
		/// <summary>
		/// Accepts the candidate's id, deletes the candidate and whatever info is joined with his in CandidateAddresses and CandidatePhotoIds.
		/// </summary>
		[Authorize(Roles = "1")]
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
		//CANDIDATES POST ACTIONS END

		//CANDIDATES PUT ACTIONS START
		/// <summary>
		/// Gets the user's id from jwt token, lets him edit his own candidate infos.
		/// </summary>
		[Authorize(Roles = "1")]
		[HttpPut]
		public async Task<IActionResult> Put([FromBody] CandidateUpdateDto candidateUpdateDto)
		{
			Console.WriteLine(JsonConvert.SerializeObject(candidateUpdateDto));
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

		/// <summary>
		/// Updates the information of a candidate with a certain ID, made for admin use.
		/// </summary>
		[Authorize(Roles = "1")]
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
		//CANDIDATES =PUT= ACTIONS END
	}
}
