using Microsoft.AspNetCore.Mvc;
using TestService.Models;
using TestService.Services;

namespace TestService.Controllers
{
	[ApiController]
	[Route("api/suppliers")]
	public class VendorController : ControllerBase
	{
		private readonly ILogger<VendorController> _logger;
		private readonly IVendorService _vendorService;

		public VendorController(ILogger<VendorController> logger, IVendorService vendorService)
		{
			_logger = logger;
			_vendorService = vendorService;
		}

		/// <summary>
		/// Retrieves all vendors from the configured data loader.
		/// </summary>
		/// <returns>A list of vendor DTOs.</returns>
		[HttpGet]
		public async Task<ActionResult<List<LoaderDTO>>> GetAll()
		{
			var result = await _vendorService.GetAll();
			return Ok(result);
		}

		/// <summary>
		/// Retrieves a specific vendor by ID.
		/// </summary>
		/// <param name="id">The ID of the vendor to retrieve.</param>
		/// <returns>The vendor if found; otherwise, 404 Not Found.</returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<LoaderDTO>> GetById(string id)
		{
			var result = await _vendorService.GetById(id);
			return result == null ? NotFound() : Ok(result);
		}

		/// <summary>
		/// Adds a new vendor to the configured data source.
		/// </summary>
		/// <param name="vendor">The vendor object to add.</param>
		/// <returns>A 201 Created response with the newly added vendor.</returns>
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] LoaderDTO vendor)
		{
			await _vendorService.Add(vendor);
			return CreatedAtAction(nameof(GetById), new { id = vendor.Id }, vendor);
		}

		/// <summary>
		/// Updates an existing vendor by ID.
		/// </summary>
		/// <param name="id">The ID of the vendor to update.</param>
		/// <param name="vendor">The updated vendor data.</param>
		/// <returns>A 204 No Content response on success.</returns>
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(string id, [FromBody] LoaderDTO vendor)
		{
			await _vendorService.Update(vendor);
			return NoContent();
		}

		/// <summary>
		/// Deletes a vendor by ID.
		/// </summary>
		/// <param name="id">The ID of the vendor to delete.</param>
		/// <returns>A 204 No Content response on success.</returns>
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			await _vendorService.Delete(id);
			return NoContent();
		}
	}
}
