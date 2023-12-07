using System.Net;
using LDSoft.APIClient;
using Microsoft.AspNetCore.Mvc;
using Otterly.API.ClientLib.Messages.Bingo;
using Otterly.API.Controllers.Bingo;
using Otterly.API.Tests.Helpers;
using Otterly.API.Tests.TestImplementations.Card;
using Otterly.Database.UserData.DataObjects;

namespace Otterly.API.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task CardsEmpty()
        {
            var controller = new CardController(new EmptyCardHandlerTest());
			var result = await controller.GetCards(new BaseRequest(Guid.Empty));

			var list = result.GetActionResultPayload<List<BingoCard>, OkObjectResult>(HttpStatusCode.OK);

            Assert.That(list?.Count == 0, Is.True);
		}

		[Test]
		public async Task CardDetailEmpty()
		{
			var controller = new CardController(new EmptyCardHandlerTest());
			var result = await controller.GetCardDetail(new GetCardDeatilsRequest(){UserID = Guid.Empty, CardID = 1});
            
			
			//var list = result.<GetCardDetails, OkObjectResult>(HttpStatusCode.Accepted);

			Assert.That(result.GetStatusCode<NotFoundResult>(HttpStatusCode.NotFound), Is.True);
		}

    }
}