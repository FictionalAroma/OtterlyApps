using Microsoft.EntityFrameworkCore;
using Moq;

namespace Otterly.API.Tests.Helpers
{
    //public static class DatabaseContextHelpers
    //{
    //    public static Mock<OtterlyAppsContext> GenerateDBContext()
    //    {
    //        var mockContext = new Mock<PricingDBContext>();

    //        if (fakeProducts != null)
    //        {

    //            var productSet = fakeProducts.AsQueryable().BuildMockDbSet();

    //            //https://stackoverflow.com/questions/25197513/cannot-get-dbset-find-to-work-with-moq-using-the-entity-framework
    //            productSet.Setup(m => m.Find(It.IsAny<object[]>()))
    //                .Returns<object[]>(ids => fakeProducts.FirstOrDefault(d => d.SKU == (string)ids[0]));
    //            mockContext.Setup(context => context.Products).Returns(productSet.Object);
    //        }


    //        if (fakeOffers != null)
    //        {
    //            var offerSet = fakeOffers.AsQueryable().BuildMockDbSet();
    //            offerSet.Setup(m => m.Find(It.IsAny<object[]>()))
    //                .Returns<object[]>(ids => fakeOffers.FirstOrDefault(d => d.Id == (int)ids[0]));

    //            mockContext.Setup(context => context.Offers).Returns(offerSet.Object);
    //        }

    //        return mockContext;
    //    }
    //}

    // now unused, keeping
    public static class DbSetHelpers<T> where T : class
    {

        public static Mock<DbSet<T>> CreateMockDBSet(IQueryable<T> dataList)
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(dataList.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(dataList.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(dataList.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(dataList.GetEnumerator());

            return mockSet;
        }
    }
}
