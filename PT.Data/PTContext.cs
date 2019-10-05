using Microsoft.EntityFrameworkCore;
using PT.Domain;

namespace PT.Data
{
	public class PTContext : DbContext
	{
		public PTContext(DbContextOptions<PTContext> options)
			: base(options)
		{ }

		private DbSet<Country> Country { get; set; }
		private DbSet<Company> Company { get; set; }
		private DbSet<Account> Account { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}