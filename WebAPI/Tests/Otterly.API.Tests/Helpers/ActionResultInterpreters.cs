using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Otterly.API.Tests.Helpers
{
    public static class ActionResultInterpreters
    {
        public static TPayload? GetActionResultPayload<TPayload, TActionType>(this IActionResult result,
            HttpStatusCode expectedCode) where TActionType : ObjectResult
        {
            var r = result as TActionType;

            Assert.IsNotNull(r);
            if (r == null) return default;
            Assert.That((int)expectedCode, Is.EqualTo(r.StatusCode));

            return (TPayload)r.Value!;
        }

        public static bool GetStatusCode<TActionType>(this IActionResult result,
            HttpStatusCode expectedCode) where TActionType : StatusCodeResult
        {
            var r = result as TActionType;

            Assert.IsNotNull(r);
            if (r == null) return default;
            Assert.That((int)expectedCode, Is.EqualTo(r.StatusCode));

            return (int)expectedCode == r.StatusCode;
        }

    }
}
