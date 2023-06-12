namespace Otterly.ClientLib;

public class BaseResponse
{
	public bool Success { get; set; } = true;
	public string Error { get; set; }

	public void SetError(string newError)
	{
		Error = newError;
		Success = false;
	}
}