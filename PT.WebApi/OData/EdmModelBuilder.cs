using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using PT.Domain;

namespace PT.WebApi.OData
{
	public static class EdmModelBuilder
	{
		public static IEdmModel CreateModel()
		{
			var builder = new ODataConventionModelBuilder();
			builder.EnableLowerCamelCase();
			builder.Namespace = "PT";
			builder.ContainerName = "PtContainer";

			// Anagrafica
			builder.EntitySet<Country>("Countries");
			builder.EntitySet<Company>("Companies");
			builder.EntitySet<Account>("Accounts");
			builder.EntitySet<Service>("Services");

			return builder.GetEdmModel();
		}
	}
}