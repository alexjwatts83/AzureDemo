using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

[assembly: FunctionsStartup(typeof(AzureDemo.Functions.Startup))]
namespace AzureDemo.Functions
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			var configuration = new ConfigurationBuilder()
				.AddUserSecrets(Assembly.GetExecutingAssembly(), false)
				.AddEnvironmentVariables()
				.Build();

			builder.Services.Replace(ServiceDescriptor.Singleton(typeof(IConfiguration), configuration));
		}
	}
}
