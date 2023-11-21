using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LDSoft.EntityFramework;

public class UnitOfWork : IUnitOfWork
{
	private readonly DbContext _dbContext;
	public UnitOfWork(DbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task Commit()
	{
		await _dbContext.SaveChangesAsync();
	}

	public T Repository<T>() where T : Repository
	{
		return new T(_dbContext);
	}


	private bool _disposed;
	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				_dbContext.Dispose();
			}
		}
		_disposed = true;
	}
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}