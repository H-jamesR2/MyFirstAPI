using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using MyFirstAPI.Models;

// Tools -> Manage Nuget Packages ->
namespace MyFirstAPI.Models
{
	public class MyFirstAPIDBContext : DbContext
	{	
		protected readonly IConfiguration Configuration;

		public MyFirstAPIDBContext(DbContextOptions<MyFirstAPIDBContext> options, IConfiguration configuration)
			: base (options)
		{
			Configuration = configuration;
		}	

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			//connection string --> different
			var connectionString = Configuration.GetConnectionString("CustomerDataService");
			options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

		}

		public DbSet<Customer> Customers { get; set; } = null;
		public DbSet<Email> Emails { get; set; } = null;
	}
}

