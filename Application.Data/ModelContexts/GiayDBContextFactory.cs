using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Application.Data.ModelContexts
{
	public class GiayDBContextFactory : IDesignTimeDbContextFactory<GiayDBContext>
	{
		public GiayDBContext CreateDbContext(string[] args)
		{
			// Build configuration
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			// Create DbContextOptionsBuilder
			var builder = new DbContextOptionsBuilder<GiayDBContext>();
			var connectionString = configuration.GetConnectionString("DatabaseBanGiay");

			// Configure the context to use SQL Server with the connection string
			builder.UseSqlServer(connectionString);

			return new GiayDBContext(builder.Options);
		}
	}
}
