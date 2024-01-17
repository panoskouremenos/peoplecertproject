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
		[Authorize]
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
				.Distinct() // Ensure unique product IDs
				.ToListAsync();

			return Ok(purchases);
		}

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

	}
}

