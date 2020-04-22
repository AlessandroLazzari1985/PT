using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PT.Data;
using PT.WebApi.Middleware;
using PT.WebApi.OData;

namespace PT.WebApi
{
	// Prova per creare un commit
	// Questa è la MasterHofFix Step I
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers(mvcOptions =>
				mvcOptions.EnableEndpointRouting = false);


			services.AddOData();

			services.RegisterData();
			// services.AddAuthentication(IISDefaults.AuthenticationScheme);

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

			var connectionString = "Data Source=LAPTOP-A0P4BIUQ;Initial Catalog=PersonalTrainer;Integrated Security=True";

			services.AddDbContext<PTContext>(option => option
				.UseSqlServer(connectionString)
				.EnableSensitiveDataLogging());
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseOptions();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			// Faccio la mia bella hotfix!
			app.UseCors(opt => opt
				.WithOrigins("http://localhost:4200", "http://localhost")
				.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowCredentials());

			app.UseMvc(routeBuilder =>
			{
				routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");

				// Enable full OData queries, you might want to consider which would be actually enabled in production scenaries
				routeBuilder.Count().Filter().OrderBy().Expand().Select().MaxTop(null);

				routeBuilder.MapODataServiceRoute("ODataRoute", "odata", EdmModelBuilder.CreateModel());
			});
		}
	}
}
