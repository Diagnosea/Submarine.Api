using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Authentication.Queries.CompareHashText;
using NUnit.Framework;

namespace Domain.Authentication.UnitTests.Queries.CompareHashText
{
    [TestFixture]
    public class CompareHashTextQueryHandlerTests
    {
        private CompareHashTextQueryHandler _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new CompareHashTextQueryHandler();
        }

        public class Handle : CompareHashTextQueryHandlerTests
        {
            [Test]
            public async Task GivenTextAndHash_ReturnsTrueIfTextIsValid()
            {
                // Act
                var cancellationToken = new CancellationToken();
                
                const string password = "This is a password";

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var query = new CompareHashTextQuery
                {
                    Hash = hashedPassword,
                    Text = password
                };
                
                // Act
                var result = await _classUnderTest.Handle(query, cancellationToken);
                
                // Assert
                Assert.That(result, Is.True);
            }

            [Test]
            public async Task GivenTextAndHash_ReturnFalseIfTextIsInvalid()
            {
                // Act
                var cancellationToken = new CancellationToken();
                
                const string password = "This is a password";

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var query = new CompareHashTextQuery
                {
                    Hash = hashedPassword,
                    Text = "This is not the same password"
                };
                
                // Act
                var result = await _classUnderTest.Handle(query, cancellationToken);
                
                // Assert
                Assert.That(result, Is.False);
            }
        }
    }
}