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

		[HttpGet]
		public ActionResult<List<LoaderDTO>> GetAll()
		{
			return Ok(_vendorService.GetAll());
		}

		[HttpGet("{id}")]
		public ActionResult<LoaderDTO> GetById(string id)
		{
			var result = _vendorService.GetById(id);
			return result == null ? NotFound() : Ok(result);
		}
		[HttpPost]
		public IActionResult Add([FromBody] LoaderDTO vendor)
		{
			_vendorService.Add(vendor);
			return CreatedAtAction(nameof(GetById), new { id = vendor.Id }, vendor);
		}

		[HttpPut("{id}")]
		public IActionResult Update(string id, [FromBody] LoaderDTO vendor)
		{
			_vendorService.Update(vendor);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(string id)
		{
			_vendorService.Delete(id);
			return NoContent();
		}

	}
}
