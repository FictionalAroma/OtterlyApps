using Microsoft.AspNetCore.Mvc;

namespace Otterly.API.Controllers;

public class Test : ControllerBase
{
	[HttpGet]
	public string TestGet()
	{
		return "Basic Test Get";
	}
	
	[HttpGet]
	[Route("OtherTestGet")]
	public string OtherGetTest()
	{
		return "Other Test Get";
	}

}