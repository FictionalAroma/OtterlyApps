using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otterly.Database.ActivityData.Interfaces;

public interface IMongoServiceBase<T> where T : MongoDataEntry
{
    Task<List<T>> GetAsync();
    Task<T?> GetAsync(string id);
    Task CreateAsync(T newT);
    Task UpdateAsync(string id, T updatedT);
    Task RemoveAsync(string id);
	Task UpdateListAsync(List<T> updated);
}