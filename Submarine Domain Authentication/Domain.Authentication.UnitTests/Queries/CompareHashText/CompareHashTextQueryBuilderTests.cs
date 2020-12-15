using Diagnosea.Submarine.Domain.Authentication.Queries.CompareHashText;
using NUnit.Framework;

namespace Domain.Authentication.UnitTests.Queries.CompareHashText
{
    [TestFixture]
    public class CompareHashTextQueryBuilderTests
    {
        public class WithHash : CompareHashTextQueryBuilderTests
        {
            [Test]
            public void GivenHash_BuildsWithHash()
            {
                // Arrange
                var builder = new CompareHashTextQueryBuilder();
                const string hash = "This is a hash";
                
                // Act
                var resultingReturn = builder.WithHash(hash);
                var resultingBuild = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(resultingReturn, Is.EqualTo(builder));
                    Assert.That(resultingBuild.Hash, Is.EqualTo(hash));
                });
            }
        }

        public class WithText : CompareHashTextQueryBuilderTests
        {
            [Test]
            public void GivenText_BuildsWithText()
            {
                // Arrange
                var builder = new CompareHashTextQueryBuilder();
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