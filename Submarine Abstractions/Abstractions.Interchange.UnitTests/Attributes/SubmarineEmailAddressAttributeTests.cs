using Abstractions.Exceptions.Messages;
using Diagnosea.Submarine.Abstractions.Interchange.Attributes;
using NUnit.Framework;

namespace Diagnosea.Submarine.Abstractions.Interchange.UnitTests.Attributes
{
    [TestFixture]
    public class SubmarineEmailAddressAttributeTests
    {
        public class FormatErrorMessage : SubmarineEmailAddressAttributeTests
        {
            [Test]
            public void GivenErrorMessageIsAlreadySet_ReturnsAlreadySetErrorMessage()
            {
                // Arrange
                var exception = new SubmarineEmailAddressAttribute {ErrorMessage = "This is an error message"};
                
                // Act
                var result = exception.FormatErrorMessage("name");
                
                // Assert
                Assert.That(result, Is.EqualTo(exception.ErrorMessage));
            }

            [Test]
            public void GivenErrorMessageIsntAlreadySet_ReturnsInterchangeExceptionMessage()
            {
                // Arrange
                var exception = new SubmarineEmailAddressAttribute();
                
                // Act
                var result = exception.FormatErrorMessage("name");
                
                // Assert
                Assert.That(result, Is.EqualTo(InterchangeExceptionMessages.InvalidEmailAddress));
            }
        }
    }
}