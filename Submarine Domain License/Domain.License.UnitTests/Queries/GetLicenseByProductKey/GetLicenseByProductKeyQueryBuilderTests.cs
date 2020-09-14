using Diagnosea.Submarine.Domain.License.Queries.GetLicenseByProductKey;
using NUnit.Framework;

namespace Submarine.Domain.License.UnitTests.Queries.GetLicenseByProductKey
{
    [TestFixture]
    public class GetLicenseByProductKeyQueryBuilderTests
    {
        public class WithProductKey : GetLicenseByProductKeyQueryBuilderTests
        {
            [Test]
            public void GivenProductKey_BuildsWithProductKey()
            {
                // Arrange
                var builder = new GetLicenseByProductKeyQueryBuilder();
                const string productKey = "This is the product key";
                
                // Act
                var result = builder.WithProductKey(productKey);
                var build = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.EqualTo(builder));
                    Assert.That(build.ProductKey, Is.EqualTo(productKey));
                });
            }
        }
    }
}