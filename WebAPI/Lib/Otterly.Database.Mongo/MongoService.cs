using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Otterly.Database.ActivityData.Configuration;

namespace Otterly.Database.ActivityData;

public class MongoService<T> where T : MongoDataEntry
{
	protected readonly IMapper Mapper;
	protected readonly IMongoCollection<T> Collection;

	public MongoService(IOptions<MongoDBConfig> config, MongoClient client, string collectionName, IMapper mapper)
	{
		Mapper = mapper;
		var dbConn = client.GetDatabase(config.Value.DatabaseName);
		Collection = dbConn.GetCollection<T>(collectionName);
	}

	public async Task<List<T>> GetAsync() =>
		await Collection.Find(_ => true).ToListAsync();

	public async Task<T?> GetAsync(string id) =>
		await Collection.Find(x => x.Id == id).FirstOrDefaultAsync();

	public async Task CreateAsync(T newT) =>
		await Collection.InsertOneAsync(newT);

	public async Task UpdateAsync(string id, T updatedT) =>
		await Collection.ReplaceOneAsync(x => x.Id == id, updatedT);

	public async Task RemoveAsync(string id) =>
		await Collection.DeleteOneAsync(x => x.Id == id);
}