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
    public class ExamVouchersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExamVouchersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ExamVouchers
        [HttpGet]
        public IActionResult GetExamVouchers()
        {
            if (_context.ExamVouchers == null)
            {
                return NotFound();
            }

            var examVoucher = _context.ExamVouchers
                  .Include(c => c.Certificate);

            return Ok(examVoucher);
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamVoucher(int id, ExamVoucherDto examVoucherDto)
        {
            var examVoucher = _context.ExamVouchers.FirstOrDefault(c => c.VoucherId == id);

            if (examVoucher == null)
            {
                return NotFound();
            }

            examVoucher.ProductId = examVoucherDto.ProductId;
            examVoucher.CandidateId = examVoucherDto.CandidateId;
            examVoucher.CertificateId = examVoucherDto.CertificateId;
            examVoucher.VoucherCode = examVoucherDto.VoucherCode;
            examVoucher.IsUsed = examVoucher.IsUsed;

            _context.SaveChanges();
            return Ok(examVoucher);
        }

        // POST: api/ExamVouchers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostExamVoucher(ExamVoucherDto examVoucherdto)
        {
            if (_context.ExamVouchers == null)
            {
                return Problem("Entity is null.");
            }

            ExamVoucher examVoucher = new ExamVoucher()
            {
                ProductId = examVoucherdto.ProductId,
                CandidateId = examVoucherdto.CandidateId,
                CertificateId = examVoucherdto.CertificateId,
                VoucherCode = examVoucherdto.VoucherCode,
                IsUsed = examVoucherdto.IsUsed

            };

            _context.ExamVouchers.Add(examVoucher);
            _context.SaveChanges();

            return Ok(examVoucher);

        }

        // DELETE: api/ExamVouchers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteExamVoucher(int id)
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

            _context.ExamVouchers.Remove(examVoucher);
            _context.SaveChanges();

            return Ok(examVoucher);
        }

        private bool ExamVoucherExists(int id)
        {
            return (_context.ExamVouchers?.Any(e => e.VoucherId == id)).GetValueOrDefault();
        }
    }
}
