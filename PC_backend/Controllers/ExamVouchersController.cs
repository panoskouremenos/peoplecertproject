using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class ExamVouchersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExamVouchersController(ApplicationDbContext context)
        {
            _context = context;
        }

		[HttpGet]
		public IActionResult GetExamVouchers()
		{
			if (_context.ExamVouchers == null)
			{
				return NotFound();
			}

			var examVouchers = _context.ExamVouchers
				.Include(ev => ev.Certificate)
				.Include(ev => ev.Candidate)
				.ThenInclude(c => c.User) 
				.Select(ev => new
				{
					VoucherID = ev.VoucherId,
					CertificateTitle = ev.Certificate.Title,
					IsUsed = ev.IsUsed,
					VoucherCode = ev.VoucherCode,
					UserName = ev.Candidate.User.UserName 
				})
				.ToList();


			return Ok(examVouchers);
		}

			// GET: api/ExamVouchers/5
			[HttpGet("{id}")]
        public IActionResult GetExamVoucher(int id)
        {
            if (_context.ExamVouchers == null)
            {
                return NotFound();
            }

            var examVoucher = _context.ExamVouchers.Find(id);

            if (examVoucher == null)
            {
                return NotFound();
            }

            return Ok(examVoucher);
        }

        // PUT: api/ExamVouchers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutExamVoucher(int id, ExamVoucherDto examVoucherDto)
        //{
        //    var examVoucher = _context.ExamVouchers.FirstOrDefault(c => c.VoucherId == id);

        //    if (examVoucher == null)
        //    {
        //        return NotFound();
        //    }

        //    examVoucher.ProductId = examVoucherDto.ProductId;
        //   // examVoucher.CandidateId = examVoucherDto.CandidateId;
        //    examVoucher.CertificateId = examVoucherDto.CertificateId;
        //    examVoucher.VoucherCode = examVoucherDto.VoucherCode;
        //    examVoucher.IsUsed = examVoucher.IsUsed;

        //    _context.SaveChanges();
        //    return Ok(examVoucher);
        //}

		// POST: api/ExamVouchers
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public IActionResult PostExamVoucher(ExamVoucherDto examVoucherDto)
		{
			// Find the ProductID from EshopProduct table based on CertificateId
			var product = _context.EshopProducts
								  .FirstOrDefault(p => p.CertificateId == examVoucherDto.CertificateId);

			if (product == null)
			{
				return NotFound("No product found for the given certificate ID.");
			}

			// Generate a new UUID (VoucherCode)
			Guid voucherCode = Guid.NewGuid();

			// Create ExamVoucher
			ExamVoucher examVoucher = new ExamVoucher()
			{
				ProductId = product.ProductId,
				CandidateId = 2,
				CertificateId = examVoucherDto.CertificateId,
				VoucherCode = voucherCode,
				IsUsed = false
			};

			_context.ExamVouchers.Add(examVoucher);
			_context.SaveChanges();

			return Ok(new { message = "Exam voucher created successfully.", voucherCode });
		}

		[Authorize]
		[HttpPut("RedeemVoucher/{voucherCode}")]
		public IActionResult RedeemVoucher(Guid voucherCode, [FromBody] RedeemVoucherDto redeemVoucherDto)
		{
			var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
			if (userIdClaim == null)
			{
				return Unauthorized("User ID is missing.");
			}

			var userId = int.Parse(userIdClaim.Value);
			var candidate = _context.Candidates.FirstOrDefault(c => c.UserId == userId);

			if (candidate == null)
			{
				return NotFound("Candidate not found.");
			}
			var voucher = _context.ExamVouchers.FirstOrDefault(v => v.VoucherCode == voucherCode);
			Console.WriteLine($"Received date: {redeemVoucherDto.DateAssigned}");

			Console.WriteLine($"Voucher: {voucher?.VoucherId}, IsUsed: {voucher?.IsUsed}");



			if (voucher == null || voucher.IsUsed)
			{

				return Unauthorized("Voucher not found or already used.");
			}

			// Update the ExamVoucher entry
			voucher.CandidateId = candidate.CandidateId;
			voucher.IsUsed = true;
			_context.SaveChanges();

			// Create a new Exam entry
			Exam exam = new Exam
			{
				CandidateId = candidate.CandidateId,
				VoucherId = voucher.VoucherId,
				DateAssigned = redeemVoucherDto.DateAssigned,
				CertificateId = voucher.CertificateId
			};

			_context.Exams.Add(exam);
			_context.SaveChanges();

			return Ok("Voucher redeemed and exam scheduled.");
		}
	}
}
