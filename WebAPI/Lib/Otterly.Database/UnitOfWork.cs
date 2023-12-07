using Otterly.Database.UserData.Interfaces;
using Otterly.Database.UserData.Repositories;

namespace Otterly.Database.UserData;

public class UnitOfWork : IDisposable
{
	private OtterlyAppsContext _context;

    private bool disposed;

	private Lazy<OtterlyAppsUserRepo> _userRepo;
	public IOtterlyAppsUserRepo UserRepo => _userRepo.Value;

	private Lazy<BingoCardRepo> _cardRepo;
	public IBingoCardRepo BingoCardRepo => _cardRepo.Value;
	private Lazy<VerificationQueueRepo> _verificationRepo;
	public IVerificationQueueRepo VerificationRepo => _verificationRepo.Value;

	public UnitOfWork(OtterlyAppsContext context)
	{
		_context = context;
		_userRepo = new Lazy<OtterlyAppsUserRepo>(Repository<OtterlyAppsUserRepo>);
		_cardRepo = new Lazy<BingoCardRepo>(Repository<BingoCardRepo>);
		_verificationRepo = new Lazy<VerificationQueueRepo>(Repository<VerificationQueueRepo>);
	}

    private T Repository<T>() where T : BaseRepo, new()
	{
		return new T { Context = _context };
	}

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

	protected virtual void Dispose(bool disposing)
	{
		if (!this.disposed)
		{
			if (disposing)
			{
				_context.Dispose();
			}
		}
		this.disposed = true;
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}
