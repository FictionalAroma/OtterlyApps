namespace LDSoft.APIClient;

public class BaseRequest
{
	public BaseRequest() { }
	public BaseRequest(Guid userID) { UserID = userID; }
	public Guid UserID { get; set; }
}