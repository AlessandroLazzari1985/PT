using Microsoft.Extensions.DependencyInjection;
using PT.Data.Repositories;

namespace PT.Data
{
	public static class Container
	{
		public static IServiceCollection RegisterData(this IServiceCollection collection)
		{
			collection.AddScoped<AccountsRepository>();
			collection.AddScoped<CompaniesRepository>();
			collection.AddScoped<ServiceRepository>();

			return collection;
		}
	}
}
