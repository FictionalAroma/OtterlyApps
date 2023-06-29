using System;

namespace Otterly.API.ClientLib;

public class BaseRequest
{
	public BaseRequest() { }
	public BaseRequest(Guid userID) { UserID = userID; }
	public Guid UserID { get; set; }
}