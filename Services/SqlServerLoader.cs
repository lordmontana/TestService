using SqlServerLoader;
using TestService.Exceptions;
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
			try
			{
				var traders = await loader.LoadTraders();

				return traders.Select(t => new LoaderDTO
				{
					Id = t.Code,
					Name = t.Description,
					Region = t.Street
				}).ToList();
			}
			catch (Exception ex) when (ex.Message.Contains("Wrong connection info"))
			{

				throw new CustomException("Wrong connection info");
			}

		}

		/// <inheritdoc/>
		public async Task<LoaderDTO> GetById(string id)
		{
			var loader = new DataLoader(_server, _userId, _password);

			try
			{
				var trader = await loader.LoadTrader(id);

				return new LoaderDTO
				{
					Id = trader.Code,
					Name = trader.Description,
					Region = trader.Street
				};
			}
			catch (Exception ex) when (
			ex.Message.Contains("Trader not found") || 
			ex.Message.Contains("Wrong connection info"))
			{
				throw new CustomException("Supplier not found");
			}
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

			try
			{
				loader.InsertTrader(trader);
				return Task.CompletedTask;
			}
			catch (Exception ex) when (
				ex.Message.Contains("Code and description are required") ||
				ex.Message.Contains("Trader already exists") ||
				ex.Message.Contains("Wrong connection info"))

			{
				throw new CustomException(ex.Message);
			}
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

			try
			{
				loader.UpdateTrader(traderToUpdate);
				return Task.CompletedTask;
			}
			catch (Exception ex) when (
			ex.Message.Contains("Trader not found")||
			ex.Message.Contains("Wrong connection info"))                       
			{
				throw new CustomException(ex.Message);
			}
		}


		/// <inheritdoc/>
		public Task Delete(string id)
		{
			var loader = new DataLoader(_server, _userId, _password);

			try
			{
				loader.DeleteTrader(id);
				return Task.CompletedTask;
			}
			catch (Exception ex) when (
			ex.Message.Contains("Trader not found")||
			ex.Message.Contains("Wrong connection info"))
			{
				throw new CustomException(ex.Message);
			}
		}

	}
}
