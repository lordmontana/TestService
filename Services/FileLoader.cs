using TestService.Models;
using FileLoader;

namespace TestService.Services
{
	/// <inheritdoc/>
	public class FileLoader : IVendorService
	{
		private readonly string? _path;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileLoader"/> class.
		/// </summary>
		/// <param name="config">Application configuration used to retrieve the file path.</param>
		public FileLoader(IConfiguration config)
		{
			_path = config["VendorSettings:FileLoader:FilePath"];
		}

		/// <inheritdoc/>
		public Task<List<LoaderDTO>> GetAll()
		{
			var loader = new Loader(_path);
			var rawList = loader.LoadSuppliers(); 

			var result = rawList.Select(s => new LoaderDTO
			{
				Id = s.Id,
				Name = s.Name,
				Region = s.Address,
			}).ToList();

			return Task.FromResult(result);
		}

		/// <inheritdoc/>
		public Task<LoaderDTO> GetById(string id)
		{
			var loader = new Loader(_path);
			var item = loader.LoadSupplier(id); 

			var dto = item == null ? new LoaderDTO() : new LoaderDTO
			{
				Id = item.Id,
				Name = item.Name,
				Region = item.Address
			};

			return Task.FromResult(dto);
		}

		/// <inheritdoc/>
		public Task Add(LoaderDTO supplier)
		{
			var loader = new Loader(_path);
			var supplierToInsert = new Supplier
			{
				Id = supplier.Id,
				Name = supplier.Name,
				Address = supplier.Region
			};
			loader.InsertSupplier(supplierToInsert);

			return Task.CompletedTask;
		}

		/// <inheritdoc/>
		public Task Update(LoaderDTO supplier)
		{
			var loader = new Loader(_path);
			var supplierToUpdate = new Supplier
			{
				Id = supplier.Id,
				Name = supplier.Name,
				Address = supplier.Region
			};
			loader.UpdateSupplier(supplierToUpdate);

			return Task.CompletedTask;
		}

		/// <inheritdoc/>
		public Task Delete(string id)
		{
			var loader = new Loader(_path);
			loader.DeleteSupplier(id);

			return Task.CompletedTask;
		}
	}
}
