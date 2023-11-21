namespace LDSoft.EntityFramework
{
	public interface IUnitOfWork : IDisposable
	{
		Task Commit();
		IRepository<T> Repository<T>() where T : class;
	}
}