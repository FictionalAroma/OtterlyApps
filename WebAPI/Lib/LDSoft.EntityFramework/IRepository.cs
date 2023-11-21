namespace LDSoft.EntityFramework;

public interface IRepository<T> where T : class
{
	Task<T?> GetById(object? id);
	Task<List<T>> GetAll();
	Task Add(T entity);
}