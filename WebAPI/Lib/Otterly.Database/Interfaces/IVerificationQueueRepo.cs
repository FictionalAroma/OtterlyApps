using Otterly.Database.UserData.DataObjects;

namespace Otterly.Database.UserData.Interfaces;

public interface IVerificationQueueRepo
{
	Task<VerificationQueueItem?> GetActiveVerification(string sessionId, int requestVerificationID);
	Task<List<VerificationQueuePlayerLog>> GetAllTicketsForQueueItem(int markedItemVerificationID);

	Task<List<VerificationQueueItem>> GetNonExpiredVerifications(string sessionID);
}