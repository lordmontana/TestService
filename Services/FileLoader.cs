using FileLoader;
using TestService.Exceptions;
using TestService.Models;

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
			try
			{
				var item = loader.LoadSupplier(id); 

				var dto = new LoaderDTO
				{
					Id = item.Id,
					Name = item.Name,
					Region = item.Address
				};

				return Task.FromResult(dto);
			}
			catch (ApiException ex)
			{
				throw new CustomException(ex.Message, ex.StatusCode);
			}

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

			try
			{
				loader.InsertSupplier(supplierToInsert);
				return Task.CompletedTask;
			}
			catch (ApiException ex)
			{
				throw new CustomException(ex.Message, ex.StatusCode);
			}
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

			try
			{
				loader.UpdateSupplier(supplierToUpdate);
				return Task.CompletedTask;
			}
			catch (ApiException ex)
			{
				throw new CustomException(ex.Message, ex.StatusCode);
			}
		}


		/// <inheritdoc/>
		public Task Delete(string id)
		{
			var loader = new Loader(_path);

			try
			{
				loader.DeleteSupplier(id);
				return Task.CompletedTask;
			}
			catch (ApiException ex)
			{
				throw new CustomException(ex.Message, ex.StatusCode);
			}
		}

	}
}
