namespace TestService.Services
{
	using TestService.Models;

	/// <summary>
	/// Defines asynchronous operations for retrieving, creating, updating, and deleting vendor data.
	/// </summary>
	public interface IVendorService
	{
		/// <summary>
		/// Asynchronously retrieves all vendor records from the underlying loader.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation. The task result contains a list of vendor DTOs.</returns>
		Task<List<LoaderDTO>> GetAll();

		/// <summary>
		/// Asynchronously retrieves a specific vendor by its unique identifier.
		/// </summary>
		/// <param name="id">The vendor ID.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the matching vendor, or null if not found.</returns>
		Task<LoaderDTO> GetById(string id);

		/// <summary>
		/// Asynchronously adds a new vendor record to the data source.
		/// </summary>
		/// <param name="supplier">The vendor data to add.</param>
		Task Add(LoaderDTO supplier);

		/// <summary>
		/// Asynchronously updates an existing vendor record.
		/// </summary>
		/// <param name="supplier">The vendor data to update.</param>
		Task Update(LoaderDTO supplier);

		/// <summary>
		/// Asynchronously deletes a vendor from the data source by ID.
		/// </summary>
		/// <param name="id">The ID of the vendor to delete.</param>
		Task Delete(string id);
	}
}
