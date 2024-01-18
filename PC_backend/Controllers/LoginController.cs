using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PC_backend.Dto;
using PC_backend.Models;
using PC_backend.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private void LogJwtToken(string token)
	{
		var handler = new JwtSecurityTokenHandler();
		var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

		//Just for testing
		//Console.WriteLine("JWT Token Claims:");
		//foreach (var claim in jsonToken.Claims)
		//{
		//	Console.WriteLine($"{claim.Type}: {claim.Value}");
		//}
	}
	private readonly ApplicationDbContext _context;
	private readonly IPasswordHasher<Usertbl> _passwordHasher;
	private readonly IConfiguration _configuration;

	public AuthController(ApplicationDbContext context, IPasswordHasher<Usertbl> passwordHasher, IConfiguration configuration)
	{
		_context = context;
		_passwordHasher = passwordHasher;
		_configuration = configuration;
	}
	/// <summary>
	/// Action that makes a new user in the database. ..
	/// </summary>
	/// 
	//LOGIN =POST= ACTIONS START
	[HttpPost("register")]
	public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
	{
		if (_context.Usertbls.Any(u => u.UserName == userRegisterDto.UserName))
		{

			return BadRequest("Username is already taken. Please choose a different username.");
		}


		var user = new Usertbl
		{
			UserName = userRegisterDto.UserName,
			RoleId = 1,
			IsActive = true,
		};



		user.PasswordHash = _passwordHasher.HashPassword(user, userRegisterDto.Password);

		_context.Usertbls.Add(user);
		await _context.SaveChangesAsync();
		return Ok();
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Usertbl>>> GetUsers()
	{
		return await _context.Usertbls.ToListAsync();
	}

	/// <summary>
	/// Gives a jwt token to the user.
	/// </summary>
	[HttpPost("login")]
	public async Task<IActionResult> Login(UserLoginDto userLoginDto)
	{
		var user = await _context.Usertbls.FirstOrDefaultAsync(u => u.UserName == userLoginDto.UserName);

		if (user != null)
		{
			var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userLoginDto.Password);
			if (result == PasswordVerificationResult.Success)
			{
				var token = GenerateJwtToken(user);
				LogJwtToken(token);
				return Ok(new { Token = token });
			}
		}
		return Unauthorized("Invalid credentials");
	}
	//LOGIN =POST= ACTIONS END

	//LOGIN =GET= ACTIONS START
	/// <summary>
	/// Get the user's Candidateid (if he has one)
	/// </summary>
	[Authorize]
	[HttpGet("GetCandidateId")]
	public async Task<ActionResult<int>> GetCandidateId()
	{
		var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
		if (userIdClaim == null)
		{
			return Unauthorized("User ID is missing in the token.");
		}

		var userId = int.Parse(userIdClaim.Value);

		var candidate = await _context.Candidates
			.FirstOrDefaultAsync(c => c.UserId == userId);

		if (candidate == null)
		{
			return NotFound("Candidate not found for the user.");
		}

		return Ok(candidate.CandidateId);
	}

	[Authorize]
	[HttpGet("GetStatus")]
	public async Task<IActionResult> GetStatus()
	{
		var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
		if (userIdClaim == null)
		{
			return Unauthorized("User ID is missing in the token.");
		}

		var userId = int.Parse(userIdClaim.Value);

		var userStatus = await _context.Usertbls
			.Where(u => u.UserId == userId)
			.Select(u => new
			{
				u.RoleId,
				u.Cash,
				UserName = u.UserName,
				CandidateId = u.Candidates.Any() ? u.Candidates.First().CandidateId.ToString() : null
			})
			.FirstOrDefaultAsync();

		if (userStatus == null)
		{
			return NotFound("User not found.");
		}

		return Ok(userStatus);
	}


	//DEPRECATED
	//[Authorize]
	//[HttpGet("GetUsername")]
	//public async Task<ActionResult<string>> GetUsername()
	//{
	//	var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
	//	if (userIdClaim == null)
	//	{
	//		return Unauthorized("User ID is missing in the token.");
	//	}

	//	var userId = int.Parse(userIdClaim.Value);

	//	var user = await _context.Usertbls
	//		.FirstOrDefaultAsync(u => u.UserId == userId);

	//	if (user == null)
	//	{
	//		return NotFound("User not found.");
	//	}

	//	return Ok(user.UserName);
	//}

	//LOGIN =GET= ACTIONS END

	//LOGIN =PUT= ACTIONS START
	/// <summary>
	/// Gets the jwt's userID and lets him edit his password.
	/// </summary>
	[Authorize]
	[HttpPut("change-password")]
	public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
	{
		var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
		if (userIdClaim == null)
		{
			return Unauthorized("User ID is missing in the token.");
		}

		var userId = int.Parse(userIdClaim.Value);
		var user = await _context.Usertbls.FindAsync(userId);
		if (user == null)
		{
			return NotFound("User not found.");
		}

		user.PasswordHash = _passwordHasher.HashPassword(user, changePasswordDto.NewPassword);
		await _context.SaveChangesAsync();

		return Ok("Password changed successfully.");
	}
	/// <summary>
	/// Gets the jwt token from the authorized user, checks his userID and then changes his name, also check if the name already exists in the db.
	/// </summary>
	[Authorize]
	[HttpPut("change-username")]
	public async Task<IActionResult> ChangeUsername(ChangeUsernameDto changeUsernameDto)
	{
		var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
		if (userIdClaim == null)
		{
			return Unauthorized("User ID is missing in the token.");
		}

		var userId = int.Parse(userIdClaim.Value);
		var user = await _context.Usertbls.FindAsync(userId);
		if (user == null)
		{
			return NotFound("User not found.");
		}

		var existingUser = await _context.Usertbls
			.AnyAsync(u => u.UserName == changeUsernameDto.NewUsername && u.UserId != userId);

		if (existingUser)
		{
			return Conflict("Username already taken.");
		}

		user.UserName = changeUsernameDto.NewUsername;
		await _context.SaveChangesAsync();

		return Ok("Username changed successfully.");
	}

	[Authorize]
	[HttpPut("ChangeRole/{userId}/{roleId}")]
	public async Task<IActionResult> ChangeUserRole(int userId, int roleId)
	{
		var user = await _context.Usertbls.FindAsync(userId);
		if (user == null)
		{
			return NotFound();
		}

		var roleExists = await _context.UserRoles.AnyAsync(r => r.RoleId == roleId);
		if (!roleExists)
		{
			return BadRequest("Role does not exist.");
		}

		user.RoleId = roleId;
		await _context.SaveChangesAsync();

		return NoContent();
	}

	private string GenerateJwtToken(Usertbl user)
	{
		var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);
		var securityKey = new SymmetricSecurityKey(key);
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var claims = new[]
		{
		new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
		new Claim(ClaimTypes.Role , "1"),
		new Claim("role", ""+user.RoleId),
		new Claim("id", ""+user.UserId)
	};

		var token = new JwtSecurityToken(
			issuer: _configuration["JwtSettings:Issuer"],
			audience: _configuration["JwtSettings:Audience"],
			claims: claims,
			expires: DateTime.Now.AddHours(0.5),
			signingCredentials: credentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
	//LOGIN =PUT= ACTIONS START

}