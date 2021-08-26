using AzureDemo.Infrastructure.Persistence.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDemo.Infrastructure.DependencyInjection
{
	public static class CustomServices
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
		{
			services.Configure<ConnectionStringSettings>(config.GetSection(ConnectionStringSettings.Section));

			//services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			//services.AddScoped<ITodoService, VideoService>();

			return services;
		}
	}
}
