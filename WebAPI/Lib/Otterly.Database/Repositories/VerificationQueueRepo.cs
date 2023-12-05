using Microsoft.EntityFrameworkCore;
using Otterly.Database.UserData.DataObjects;
using Otterly.Database.UserData.Interfaces;

namespace Otterly.Database.UserData.Repositories;

public class VerificationQueueRepo : BaseRepo, IVerificationQueueRepo
{
	public async Task<VerificationQueueItem?> GetActiveVerification(string sessionId, int requestVerificationID)
	{
		return await Context.VerificationQueueItems
			.OrderBy(item => item.ActivatedDateTime)
			.FirstOrDefaultAsync(item => item.SessionID == sessionId && 
									 item.VerificationID == requestVerificationID && 
									 item.ExpiryDateTime < DateTime.Now);

	}

	public async Task<List<VerificationQueuePlayerLog>> GetAllTicketsForQueueItem(int markedItemVerificationID)
	{
		return await Context.VerificationQueuePlayerLogs.Where(log => log.VerificationID == markedItemVerificationID).ToListAsync();
	}

	public async Task<List<VerificationQueueItem>> GetNonExpiredVerifications(string sessionID)
	{
		return await Context.VerificationQueueItems.
							 Where(item => item.SessionID == sessionID && item.ExpiryDateTime > DateTime.Now)
							 .OrderBy(item => item.ActivatedDateTime)
							 .ToListAsync();
	}
}