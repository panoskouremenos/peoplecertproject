using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

		Console.WriteLine("JWT Token Claims:");
		foreach (var claim in jsonToken.Claims)
		{
			Console.WriteLine($"{claim.Type}: {claim.Value}");
		}
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
	[HttpPost("register")]
	public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
	{
		var user = new Usertbl
		{
			UserName = userRegisterDto.UserName,
			RoleId = 1,
			IsActive = 1, 
		};

		user.PasswordHash = _passwordHasher.HashPassword(user, userRegisterDto.Password);

		_context.Usertbls.Add(user);
		await _context.SaveChangesAsync();
		//testing
		return Ok(/*new { UserId = user.UserId }*/);
	}
	[Authorize]
	[HttpGet("AuthorizeAuthenticatedUsers")]
	public IActionResult AuthorizeAuthenticatedUSers()
	{
		return Ok("Authorized Client!");
	}

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

}
