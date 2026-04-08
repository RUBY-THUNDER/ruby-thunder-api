using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ruby.Thunder.Domain.Catalog;

namespace Ruby.Thunder.Domain.Tests
{
    [TestClass]
    public class RatingTests
    {
        [TestMethod]
        public void Can_Create_New_Rating()
        {
            // Arrange
            var rating = new Rating(1, "Mike", "Great fit!");

            // Assert
            Assert.AreEqual(1, rating.Stars);
            Assert.AreEqual("Mike", rating.UserName);
            Assert.AreEqual("Great fit!", rating.Review);
        }

        [TestMethod]
        public void Cannot_Create_Rating_With_Invalid_Stars()
        {
            try
            {
                // We try to create a bad rating
                var rating = new Rating(0, "Mike", "Great fit!");
                
                // If it succeeds and reaches this line, the test failed
                Assert.Fail("An ArgumentException should have been thrown.");
            }
            catch (ArgumentException)
            {
                // If it catches the error (which it should!), the test passes!
            }
        }
    }
}