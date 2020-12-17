using System;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Abstractions.Interchange.Attributes;
using NUnit.Framework;

namespace Diagnosea.Submarine.Abstractions.Interchange.UnitTests.Attributes
{
    [TestFixture]
    public class DiagnoseaRequiredAttributeTests
    {
        public class IsValid : DiagnoseaRequiredAttributeTests
        {
            [Test]
            public void GivenInvalidGuid_ReturnsFalse()
            {
                // Arrange
                var attribute = new DiagnoseaRequiredAttribute();
                
                // Act
                var result = attribute.IsValid(null);
                
                // Assert
                Assert.That(result, Is.False);
            }

            [Test]
            public void GivenEmptyGuid_ReturnsFalse()
            {
                // Arrange
                var attribute = new DiagnoseaRequiredAttribute();
                
                // Act
                var result = attribute.IsValid(Guid.Empty);
                
                // Assert
                Assert.That(result, Is.False);
            }
        }
        
        public class FormatErrorMessage : DiagnoseaRequiredAttributeTests
        {
            [Test]
            public void GivenErrorMessageIsAlreadySet_ReturnsAlreadySetErrorMessage()
            {
                // Arrange
                var attribute = new DiagnoseaRequiredAttribute {ErrorMessage = "This is an error message"};
                
                // Act
                var result = attribute.FormatErrorMessage("name");
                
                // Assert
                Assert.That(result, Is.EqualTo(attribute.ErrorMessage));
            }

            [Test]
            public void GivenErrorMessageIsntAlreadySet_ReturnsInterchangeExceptionMessage()
            {
                // Arrange
                var attribute = new DiagnoseaRequiredAttribute();
                
                // Act
                var result = attribute.FormatErrorMessage("name");
                
                // Assert
                Assert.That(result, Is.EqualTo(ExceptionMessages.Interchange.Required));
            }
        }
    }
}