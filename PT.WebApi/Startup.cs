using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PT.Data;
using PT.WebApi.Middleware;
using PT.WebApi.OData;

namespace PT.WebApi
{
	public class Startup
	{
		readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddOData();

			services.RegisterData();
			// services.AddAuthentication(IISDefaults.AuthenticationScheme);

			services.AddMvc(config =>
			{
				// NON SO COSA FACCIA; MA PERMETTE DI ESEGUIRE LE RICHIESTE OPTIONS SENZA AUTENTICAZIONE
				//var policy = new AuthorizationPolicyBuilder()
				//	.RequireAuthenticatedUser()
				//	.Build();
				//config.Filters.Add(new AuthorizeFilter(policy));
			}).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


			services.AddCors(options =>
			{
				options.AddPolicy(MyAllowSpecificOrigins,
					builder =>
					{
						builder
							// TODO: Migliorare
							.WithOrigins(
								"http://localhost",
								"http://localhost:4200"
								//"http://localhost:8090",
								//"http://sfamlu01",
								//"http://sfamlu01:8020",
								//"http://sfamlu01:8090",
								//"http://sfamlu02",
								//"http://sfamlu02:8020",
								//"http://sfamlu02:8090",
								//"http://sfamlu03",
								//"http://sfamlu03:8020",
								//"http://sfamlu03:8090"
								)
							.AllowAnyMethod()
							.AllowAnyHeader()
							.AllowCredentials();
					});
			});


			var connectionString = "Data Source=LAPTOP-A0P4BIUQ;Initial Catalog=PersonalTrainer;Integrated Security=True";

			services.AddDbContext<PTContext>(option => option
				.UseSqlServer(connectionString)
				.EnableSensitiveDataLogging());
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseOptions();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(MyAllowSpecificOrigins);
			// app.UseStaticFiles();

			app.UseMvc(routeBuilder =>
			{
				routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");

				// Enable full OData queries, you might want to consider which would be actually enabled in production scenaries
				routeBuilder.Count().Filter().OrderBy().Expand().Select().MaxTop(null);

				routeBuilder.MapODataServiceRoute("ODataRoute", "odata", EdmModelBuilder.CreateModel());

				// Work-around for #1175
				routeBuilder.EnableDependencyInjection();
			});

			//app.Run(async (context) =>
			//{
			//	await context.Response.WriteAsync("Hello World!");
			//});
		}
	}
}
