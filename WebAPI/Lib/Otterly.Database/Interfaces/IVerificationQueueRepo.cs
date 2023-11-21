using Otterly.Database.UserData.DataObjects;

namespace Otterly.Database.UserData.Interfaces;

public interface IVerificationQueueRepo
{
	Task<VerificationQueueItem?> GetActiveVerification(string sessionId, int requestVerificationID);
}