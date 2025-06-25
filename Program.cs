
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

			builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
