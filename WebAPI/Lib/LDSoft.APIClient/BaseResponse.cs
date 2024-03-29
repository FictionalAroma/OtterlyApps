﻿namespace LDSoft.APIClient;

public class BaseResponse
{
	public static BaseResponse SuccessfulRequest => new BaseResponse();
	public BaseResponse() { }

	public BaseResponse(string error)
	{
		SetError(error);
	}
	public bool Success { get; set; } = true;
	public string Error { get; set; } = string.Empty;

	public void SetError(string newError)
	{
		Error = newError;
		Success = false;
	}
}