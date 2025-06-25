using TestService.Models;

namespace TestService.Services
{
	public class LoaderAService : IVendorService

	{
		private readonly string? _server;
		private readonly string? _userId;
		private readonly string? _password;

		public LoaderAService(IConfiguration config)
		{
			_server = config["VendorSettings:LoaderB:Server"];
			_userId = config["VendorSettings:LoaderB:UserId"];
			_password = config["VendorSettings:LoaderB:Password"];
		}

		public void Add(LoaderDTO supplier)
		{
			throw new NotImplementedException();
		}

		public void Delete(string id)
		{
			throw new NotImplementedException();
		}

		public List<LoaderDTO> GetAll()
		{
			throw new NotImplementedException();
		}

		public LoaderDTO GetById(string id)
		{
			throw new NotImplementedException();
		}

		public void Update(LoaderDTO supplier)
		{
			throw new NotImplementedException();
		}
	}


}
