using Microsoft.EntityFrameworkCore;
using PT.Domain;

namespace PT.Data
{
	public class PTContext : DbContext
	{
		public PTContext(DbContextOptions<PTContext> options)
			: base(options)
		{ }

		public DbSet<Country> Country { get; set; }
		public DbSet<Company> Company { get; set; }
		public DbSet<Account> Account { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}