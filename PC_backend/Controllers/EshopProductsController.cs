using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PC_backend.Dto;
using PC_backend.Models;
using PC_backend.Services;

namespace PC_backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EshopProductsController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public EshopProductsController(ApplicationDbContext context)
		{
			_context = context;
		}
		//TOPIC =GET= ACTIONS START
		[HttpGet]
		public IActionResult GetEshopProducts()
		{
			if (_context.EshopProducts == null)
			{
				return NotFound();
			}
			return Ok(_context.EshopProducts);

		}

		[HttpGet("{id}")]
		public IActionResult GetEshopProducts(int id)
		{
			if (_context.EshopProducts == null)
			{
				return NotFound();
			}

			var EshopProduct = _context.EshopProducts.FirstOrDefault(c => c.ProductId == id);

			if (EshopProduct == null)
			{
				return NotFound();
			}

			return Ok(EshopProduct);
		}
		[Authorize(Roles = "1")]
		[HttpGet("MyPurchases")]
		public async Task<IActionResult> GetMyPurchases()
		{
			var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
			if (userIdClaim == null)
			{
				return Unauthorized("User ID is missing in the token.");
			}

			var userId = int.Parse(userIdClaim.Value);
			var candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.UserId == userId);

			if (candidate == null)
			{
				return NotFound("Candidate not found for the user.");
			}

			var purchases = await _context.UserCertificatePurchases
				.Where(p => p.CandidateId == candidate.CandidateId)
				.Select(p => p.ProductId)
				.Distinct()
				.ToListAsync();

			return Ok(purchases);
		}
		//TOPIC =GET= ACTIONS END
		//TOPIC =PUT= ACTIONS START
		[Authorize(Roles = "2")]
		[HttpPut("{id}")]
		public IActionResult PutEshopProduct(int id, EshopProductDto eshopProductDto)
		{
			var eshopProduct = _context.EshopProducts//.Include(c => c.ExamVouchers)
				.FirstOrDefault(c => c.ProductId == id);
			if (eshopProduct == null)
			{
				return NotFound();
			}
			eshopProduct.ProductName = eshopProductDto.ProductName;
			eshopProduct.CertificateId = eshopProductDto.CertificateId;
			eshopProduct.Description = eshopProductDto.Description;
			eshopProduct.Price = eshopProductDto.Price;
			eshopProduct.AvailableStock = eshopProductDto.AvailableStock;
			eshopProduct.Deleted = eshopProductDto.Deleted;

			_context.SaveChanges();
			return Ok(eshopProduct);
		}
        //TOPIC =PUT= ACTIONS END

        //TOPIC =POST= ACTIONS START
        [Authorize(Roles = "1")]
        [HttpPost("PurchaseAndExam")]
        public async Task<IActionResult> PostPurchaseAndExam([FromBody] List<PurchaseAndExamDto> dtos)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID is missing in the token.");
            }
            var userId = int.Parse(userIdClaim.Value);

            var candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.UserId == userId);
            if (candidate == null)
            {
                return NotFound("Candidate not found for the user.");
            }

            foreach (var dto in dtos)
            {
                var purchase = new UserCertificatePurchase
                {
                    CandidateId = candidate.CandidateId,
                    ProductId = dto.ProductId,
                    PurchaseDate = dto.PurchaseDate
                };
                _context.UserCertificatePurchases.Add(purchase);
            }

            await _context.SaveChangesAsync();

            foreach (var dto in dtos)
            {
                var certificateId = await _context.EshopProducts
                    .Where(e => e.ProductId == dto.ProductId)
                    .Select(e => e.CertificateId)
                    .FirstOrDefaultAsync();

                var exam = new Exam
                {
                    CandidateId = candidate.CandidateId,
                    DateAssigned = dto.PurchaseDate,
                    CertificateId = certificateId,
                    VoucherId = null // I am setting it to null for now, if there is an error, maybe it will get generated along with others.
                };
                _context.Exams.Add(exam);
            }

            await _context.SaveChangesAsync();

            return Ok("Purchase and exam registration successful.");
        }


        [Authorize(Roles = "2")]
		[HttpPost]
		public IActionResult PostEshopProduct(EshopProductDto eshopProductDto)
		{
			if (_context.EshopProducts == null)
			{
				return Problem("Entity is null");
			}

			EshopProduct eshopProduct = new EshopProduct()
			{
				ProductName = eshopProductDto.ProductName,
				CertificateId = eshopProductDto.CertificateId,
				Description = eshopProductDto.Description,
				Price = eshopProductDto.Price,
				AvailableStock = eshopProductDto.AvailableStock,
				Deleted = eshopProductDto.Deleted
			};


			_context.EshopProducts.Add(eshopProduct);
			_context.SaveChanges();

			return Ok(eshopProduct);
		}
		//TOPIC =DELETE= ACTIONS END

		//TOPIC =DELETE= ACTIONS START
		[Authorize(Roles = "2")]
		[HttpDelete("{id}")]
		public IActionResult DeleteEshopProduct(int id)
		{
			if (_context.EshopProducts == null)
			{
				return NotFound();
			}
			var eshopProduct = _context.EshopProducts.FirstOrDefault(c => c.ProductId == id);
			if (eshopProduct == null)
			{
				return NotFound();
			}

			_context.EshopProducts.Remove(eshopProduct);
			_context.SaveChanges();
			return Ok(eshopProduct);

		}
		//TOPIC =DELETE= ACTIONS END

	}
}

