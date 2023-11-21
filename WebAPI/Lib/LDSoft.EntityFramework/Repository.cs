using Microsoft.EntityFrameworkCore;

namespace LDSoft.EntityFramework;

public class Repository<TDb>
{
	protected DbContext 
	public Repository(DbContext dbContext)
	{
	}
}

public class Repository<TObj, TDb> : Repository<TDb>, IRepository<TObj> where TObj : class
{
	private readonly DbSet<TObj> _dbSet;
	public Repository(DbContext dbContext)
	{
		_dbSet = dbContext.Set<TObj>();
	}

	public async Task<TObj?> GetById(object? id) => await _dbSet.FindAsync(id);
	
	public Task<List<TObj>> GetAll() => _dbSet.ToListAsync();
	
	public async Task Add(TObj entity) => await _dbSet.AddAsync(entity);
	
}