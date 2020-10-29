using System;
using Diagnosea.Submarine.Domain.License.Queries.GetLicenseById;
using NUnit.Framework;

namespace Submarine.Domain.License.UnitTests.Queries.GetLicenseById
{
    [TestFixture]
    public class GetLicenseByIdQueryBuilderTests
    {
        public class WithLicenseId : GetLicenseByIdQueryBuilderTests
        {
            [Test]
            public void GivenLicenseI_BuildsWithLicenseId()
            {
                // Arrange
                var builder = new GetLicenseByIdQueryBuilder();
                var licenseId = Guid.NewGuid();

                // Act
                var result = builder.WithLicenseId(licenseId);
                var build = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.EqualTo(builder));
                    Assert.That(build.LicenseId, Is.EqualTo(licenseId));
                });
            }
        }
    }
}