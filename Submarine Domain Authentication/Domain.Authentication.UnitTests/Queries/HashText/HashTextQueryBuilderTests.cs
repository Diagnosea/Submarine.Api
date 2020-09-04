using Diagnosea.Submarine.Domain.Authentication.Queries.HashText;
using NUnit.Framework;

namespace Domain.Authentication.UnitTests.Queries.HashText
{
    [TestFixture]
    public class HashTextQueryBuilderTests
    {
        public class WithText : HashTextQueryBuilderTests
        {
            [Test]
            public void GivenText_BuildsWithText()
            {
                // Arrange
                var builder = new HashTextQueryBuilder();
                const string text = "This is the text";
                
                // Act
                var resultingReturn = builder.WithText(text);
                var resultingBuild = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(resultingReturn, Is.EqualTo(builder));
                    Assert.That(resultingBuild.Text, Is.EqualTo(text));
                });
            }
        }
    }
}