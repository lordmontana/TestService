using FileLoader;
using SqlServerLoader;
using TestService.Models;

namespace TestService.Services
{
	/// <inheritdoc/>
	public class SqlServerLoader : IVendorService
	{
		private readonly string? _server;
		private readonly string? _userId;
		private readonly string? _password;

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlServerLoader"/> class with configuration values.
		/// </summary>
		public SqlServerLoader(IConfiguration config)
		{
			_server = config["VendorSettings:SqlServerLoader:Server"];
			_userId = config["VendorSettings:SqlServerLoader:UserId"];
			_password = config["VendorSettings:SqlServerLoader:Password"];
		}

		/// <inheritdoc/>
		public async Task<List<LoaderDTO>> GetAll()
		{
			var loader = new DataLoader(_server, _userId, _password);
			var traders = await loader.LoadTraders();

			return traders.Select(t => new LoaderDTO
			{
				Id = t.Code,
				Name = t.Description,
				Region = t.Street
			}).ToList();
		}

		/// <inheritdoc/>
		public async Task<LoaderDTO> GetById(string id)
		{
			var loader = new DataLoader(_server, _userId, _password);
			var item = await loader.LoadTrader(id);

			return new LoaderDTO
			{
				Id = item.Code,
				Name = item.Description,
				Region = item.Street
			};
		}

		/// <inheritdoc/>
		public Task Add(LoaderDTO supplier)
		{
			var loader = new DataLoader(_server, _userId, _password);
			var trader = new Trader
			{
				Code = supplier.Id,
				Description = supplier.Name,
				Street = supplier.Region
			};
			loader.InsertTrader(trader);
			return Task.CompletedTask;
		}

		/// <inheritdoc/>
		public Task Update(LoaderDTO supplier)
		{
			var loader = new DataLoader(_server, _userId, _password);
			var traderToUpdate = new Trader
			{
				Code = supplier.Id,
				Description = supplier.Name,
				Street = supplier.Region
			};
			loader.UpdateTrader(traderToUpdate);
			return Task.CompletedTask;
		}

		/// <inheritdoc/>
		public Task Delete(string id)
		{
			var loader = new DataLoader(_server, _userId, _password);
			loader.DeleteTrader(id);
			return Task.CompletedTask;
		}
	}
}
