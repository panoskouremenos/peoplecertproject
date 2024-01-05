﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
	/// <summary>
	/// Action that makes a new user in the database. ..
	/// </summary>
	/// <remarks>
	/// by default the isAction and roleID is "1", both RoleID and userID are passed through JWT.
	/// </remarks>
	/// <param name="userRegisterDto">Enter username and password.</param>
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
		return Ok();
	}
	/// <summary>
	/// Checks if the user is authenticated. (Mainly for testing).
	/// </summary>
	/// <remarks>
	/// This action verifies if the user is authenticated and authorized to access protected resources.
	/// It returns a simple message indicating the user is authorized.
	/// </remarks>
	/// <returns>An Ok response with a message for authorized users.</returns>
	[Authorize]
	[HttpGet("AuthorizeAuthenticatedUsers")]
	public IActionResult AuthorizeAuthenticatedUSers()
	{
		return Ok("Authorized Client!");
	}
	/// <summary>
	/// Gives a jwt token to the user.
	/// </summary>
	/// <remarks>
	/// This action gives a jwt to the user by utilizing username and password
	/// 
	/// </remarks>
	/// <returns>A jwt token for authorizarion</returns>
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
	/// <summary>
	/// Gets the jwt's userID and lets him edit his password.
	/// </summary>
	/// <remarks>
	/// 
	/// 
	/// </remarks>
	/// <returns></returns>
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
	/// <remarks>
	/// Checks if the username exists and if not it changes it.
	/// 
	/// </remarks>
	/// <returns></returns>
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