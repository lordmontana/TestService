
using TestService.Services;

namespace TestService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddScoped<IVendorService>(provider =>
            {
				var config = provider.GetRequiredService<IConfiguration>();
				var loader = config["VendorSettings:Loader"];
				return loader switch
				{
					"SqlServerLoader" => new Services.SqlServerLoader(config),
					"FileLoader" => new Services.FileLoader(config),
					_ => throw new Exception("Invalid loader config")
				};
			});


			// Add CORS policy 
			// allowing requests from the React frontend running on localhost:5173
			// needs to be configured for production/localhost as well
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowFrontend", policy =>
				{
					policy
						.WithOrigins("http://localhost:5173") // React dev server
						.AllowAnyHeader()
						.AllowAnyMethod();
				});
			});


			builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
			if (builder.Environment.EnvironmentName == "Docker")
			{
				builder.WebHost.UseUrls("http://*:80");
			}

			var app = builder.Build();

			app.UseCors("AllowFrontend");

			// Configure the HTTP request pipeline.
			//  if (app.Environment.IsDevelopment())
			// {
			app.UseSwagger();
                app.UseSwaggerUI();
          //  }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
