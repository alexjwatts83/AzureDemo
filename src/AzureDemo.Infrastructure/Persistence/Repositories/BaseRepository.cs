using AzureDemo.Application.Interfaces;
using AzureDemo.Infrastructure.Persistence.Configuration;
using AzureDemo.Infrastructure.Persistence.Extensions;
using Dapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDemo.Infrastructure.Persistence.Repositories
{
	public abstract class BaseRepository : IBaseRepository
	{
		private readonly string _connectionString;
		private readonly IDistributedCache _distributedCache;

		public IDbConnection Connection => new SqlConnection(_connectionString));

		public BaseRepository(IOptions<ConnectionStringSettings> connectionStrings,
								 IDistributedCache distributedCache)
		{
			_connectionString = connectionStrings.Value.Database;
			_distributedCache = distributedCache;
		}

		public async Task<int> ExectuteAsync(string storedProcedure, object parameters = null)
		{
			using (var connection = Connection)
			{
				return await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		public async Task<IEnumerable<T>> GetCachedDataAsync<T>(string storedProcedure, object parameters = null, int cacheDuration = 60)
		{
			string key = GetCacheKeyNameFromObject(storedProcedure, parameters);

			return await _distributedCache.GetOrCreateAsync(
				key,
				async () => await GetDataAsync<T>(storedProcedure, parameters),
				TimeSpan.FromSeconds(cacheDuration));
		}

		public async Task<IEnumerable<T>> GetDataAsync<T>(string storedProcedure, object parameters = null)
		{
			using (var connection = Connection)
			{
				return await connection.QueryAsync<T>(storedProcedure,
													  parameters,
													  commandType: CommandType.StoredProcedure)
										.ConfigureAwait(false);
			}
		}

		public async Task<T> GetSingleAsync<T>(string storedProcedure, object parameters = null)
		{
			using (var connection = Connection)
			{
				return await connection.QuerySingleOrDefaultAsync<T>(storedProcedure,
													  parameters,
													  commandType: CommandType.StoredProcedure)
										.ConfigureAwait(false);
			}
		}

		private string GetCacheKeyNameFromObject(string keyPrefix, object parameters = null)
		{
			if (parameters == null)
			{
				return keyPrefix;
			}
			// TODO: figure out this works with a datetime type
			var properties = parameters.GetType()
									   .GetProperties()
									   .Select(p => p.GetValue(parameters, null));

			return keyPrefix + string.Join("_", properties);
		}
	}
}
