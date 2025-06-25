using TestService.Models;
using FileLoader;

namespace TestService.Services
{
	public class LoaderBService : IVendorService
	{
		private readonly string? _path;
		public LoaderBService(IConfiguration config)
		{

			_path = config["VendorSettings:LoaderA:FilePath"];
		}

		public void Add(LoaderDTO supplier)
		{
			var loader = new Loader(_path);
			var supplierToUpdate = new Supplier
			{
				Id = supplier.Id,
				Name = supplier.Name,
				Address = supplier.Region
			};
			loader.InsertSupplier(supplierToUpdate);

		}

		public void Delete(string id)
		{
			var loader = new Loader(_path);
			loader.DeleteSupplier(id);	
		}

		public List<LoaderDTO> GetAll()
		{
			var loader = new Loader(_path);
			var rawList = loader.LoadSuppliers();
			return rawList.Select(s => new LoaderDTO
			{
				Id = s.Id,
				Name = s.Name,
				Region = s.Address,
			}).ToList();
		}

		public LoaderDTO GetById(string id)
		{
			var loader = new Loader(_path);
			var item = loader.LoadSupplier(id); 
			return item == null ? new LoaderDTO() : new LoaderDTO
			{
				Id = item.Id,
				Name = item.Name,
				Region = item.Address
			};
		}

		public void Update(LoaderDTO supplier)
		{
			var loader = new Loader(_path);
			var supplierToUpdate = new Supplier
			{
				Id = supplier.Id,
				Name = supplier.Name,
				Address = supplier.Region
			};
			loader.UpdateSupplier(supplierToUpdate);
		}
	}
}
