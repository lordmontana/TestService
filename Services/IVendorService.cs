namespace TestService.Services
{
	using TestService.Models;

	public interface IVendorService
	{
		List<LoaderDTO> GetAll();
		LoaderDTO GetById(string id);
		void Add(LoaderDTO supplier);
		void Update(LoaderDTO supplier);
		void Delete(string id);
	}
}
