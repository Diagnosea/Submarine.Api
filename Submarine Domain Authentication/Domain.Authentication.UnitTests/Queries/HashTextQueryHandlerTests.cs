using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Authentication.Queries.HashText;
using Diagnosea.Submarine.Domain.Authentication.Settings;
using Domain.Authentication.UnitTests.Settings;
using NUnit.Framework;

namespace Domain.Authentication.UnitTests.Queries
{
    [TestFixture]
    public class HashTextQueryHandlerTests
    {
        private ISubmarineAuthenticationSettings _submarineAuthenticationSettings;
        private HashTextQueryHandler _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _submarineAuthenticationSettings = new SubmarineTestAuthenticationSettings
            {
                SaltingRounds = 10,
            };
            
            _classUnderTest = new HashTextQueryHandler(_submarineAuthenticationSettings);
        }

        public class Handle : HashTextQueryHandlerTests
        {
            [Test]
            public async Task GivenPlainTextPassword_ReturnsEncryptedPassword()
            {
                // Arrange
                var cancellationToken = new CancellationToken();

                var query = new HashTextQuery
                {
                    PlainTextPassword = "This is a password"
                };
                
                // Act
                var result = await _classUnderTest.Handle(query, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);

                    var isValidPassword = BCrypt.Net.BCrypt.Verify(query.PlainTextPassword, result);
                    Assert.That(isValidPassword);
                });
            }
        }
    }
}