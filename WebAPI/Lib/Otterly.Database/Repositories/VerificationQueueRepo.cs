using Microsoft.EntityFrameworkCore;
using Otterly.Database.UserData.DataObjects;
using Otterly.Database.UserData.Interfaces;

namespace Otterly.Database.UserData.Repositories;

public class VerificationQueueRepo : IVerificationQueueRepo
{
	private readonly OtterlyAppsContext _context;
	public VerificationQueueRepo(OtterlyAppsContext context)
	{
		_context = context;
	}

	public async Task<VerificationQueueItem?> GetActiveVerification(string sessionId, int requestVerificationID)
	{
		return await _context.VerificationQueueItems
			.OrderBy(item => item.ActivatedDateTime)
			.FirstOrDefaultAsync(item => item.SessionID == sessionId && 
									 item.VerificationID == requestVerificationID && 
									 item.ExpiryDateTime < DateTime.Now);

	}
}