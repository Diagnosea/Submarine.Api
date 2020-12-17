using Abstractions.Exceptions;
using Diagnosea.Submarine.Abstractions.Interchange.Attributes;
using NUnit.Framework;

namespace Diagnosea.Submarine.Abstractions.Interchange.UnitTests.Attributes
{
    [TestFixture]
    public class DiagnoseaStringLengthAttributeTests
    {
        public class FormatErrorMessage : DiagnoseaAfterNowAttributeTests
        {
            [Test]
            public void GivenErrorMessageIsAlreadySet_ReturnsAlreadySetErrorMessage()
            {
                // Arrange
                var attribute = new DiagnoseaStringLengthAttribute {ErrorMessage = "This is an error message"};
                
                // Act
                var result = attribute.FormatErrorMessage("name");
                
                // Assert
                Assert.That(result, Is.EqualTo(attribute.ErrorMessage));
            }

            [Test]
            public void GivenErrorMessageIsntAlreadySet_ReturnsInterchangeExceptionMessage()
            {
                // Arrange
                var attribute = new DiagnoseaStringLengthAttribute();
                
                // Act
                var result = attribute.FormatErrorMessage("name");
                
                // Assert
                Assert.That(result, Is.EqualTo($"{ExceptionMessages.Interchange.InvalidStringLength}|0|1"));
            }
        }
    }
}