using System.IdentityModel.Tokens.Jwt;
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

namespace PC_backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VoucherController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public VoucherController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: api/voucher
		[HttpGet]
		public IActionResult GetVouchers()
		{
			if (_context.ExamVouchers == null)
			{
				return NotFound();
			}
			var exam = _context.ExamVouchers;

			return Ok(exam);
		}
	}
}
