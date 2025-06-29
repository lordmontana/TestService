using Microsoft.AspNetCore.Mvc;
using TestService.Exceptions;
using TestService.Models;
using TestService.Services.Interfaces;

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
			try
			{
				var result = await _vendorService.GetAll();
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error in GetAll");
				return StatusCode(500, "Internal server error.");
			}
		}

		/// <summary>
		/// Retrieves a specific vendor by ID.
		/// </summary>
		/// <param name="id">The ID of the vendor to retrieve.</param>
		/// <returns>The vendor if found; otherwise, 404 Not Found.</returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<LoaderDTO>> GetById(string id)
		{
			try
			{
				var result = await _vendorService.GetById(id);

				if (result == null)
					return NotFound();

				return Ok(result);
			}
			catch (CustomException ex)
			{
				return NotFound(new
				{
					message = ex.Message,
					code = ex.ErrorCode
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error in GetById");
				return StatusCode(500, "Internal server error.");
			}
		}


		/// <summary>
		/// Adds a new vendor to the configured data source.
		/// </summary>
		/// <param name="vendor">The vendor object to add.</param>
		/// <returns>A 201 Created response with the newly added vendor.</returns>
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] LoaderDTO vendor)
		{
			try
			{
				await _vendorService.Add(vendor);
				return CreatedAtAction(nameof(GetById), new { id = vendor.Id }, vendor);
			}
			catch (CustomException ex)
			{

				return BadRequest(new
				{
					message = ex.Message,
					code = ex.ErrorCode
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error in Add");
				return StatusCode(500, "Internal server error.");
			}
		}

		/// <summary>
		/// Updates an existing vendor by ID.
		/// </summary>
		/// <param name="vendor">The updated vendor data.</param>
		/// <returns>A 204 No Content response on success.</returns>
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] LoaderDTO vendor)
		{
			try
			{
				await _vendorService.Update(vendor);
				return NoContent();
			}
			catch (CustomException ex)
			{
				return BadRequest(new
				{
					message = ex.Message,
					code = ex.ErrorCode
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error in Update");
				return StatusCode(500, "Internal server error.");
			}
		}


		/// <summary>
		/// Deletes a vendor by ID.
		/// </summary>
		/// <param name="id">The ID of the vendor to delete.</param>
		/// <returns>A 204 No Content response on success.</returns>
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			try
			{
				await _vendorService.Delete(id);
				return NoContent();
			}
			catch (CustomException ex)
			{
				return BadRequest(new
				{
					message = ex.Message,
					code = ex.ErrorCode
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error in Delete");
				return StatusCode(500, "Internal server error.");
			}
		}
	}
}
