using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

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

		//public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
		//{
		//	var config = builder.ConfigurationBuilder.Build();
		//	var context = builder.GetContext();

		//	builder.ConfigurationBuilder
		//		.SetBasePath(context.ApplicationRootPath)
		//		.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
		//		.AddUserSecrets(Assembly.GetExecutingAssembly(), true)
		//		.AddEnvironmentVariables();
		//}
	}
}
