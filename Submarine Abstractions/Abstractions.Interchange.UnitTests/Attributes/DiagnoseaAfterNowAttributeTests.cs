using System;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Abstractions.Interchange.Attributes;
using NUnit.Framework;

namespace Diagnosea.Submarine.Abstractions.Interchange.UnitTests.Attributes
{
    [TestFixture]
    public class DiagnoseaAfterNowAttributeTests
    {
        public class IsValid : DiagnoseaAfterNowAttributeTests
        {
            [Test]
            public void GivenInvalidDate_ReturnsFalse()
            {
                // Arrange
                var attribute = new DiagnoseaAfterNowAttribute();
                
                // Act
                var result = attribute.IsValid(null);
                
                // Assert
                Assert.That(result, Is.False);
            }

            [Test]
            public void GivenValidDateAfterBeforeNow_ReturnsFalse()
            {
                // Arrange
                var attribute = new DiagnoseaAfterNowAttribute();
                
                // Act
                var result = attribute.IsValid(DateTime.UtcNow.AddSeconds(-20));
                
                // Assert
                Assert.That(result, Is.False);
            }

            [Test]
            public void GivenValidDateAfterNow_ReturnsTrue()
            {
                // Arrange
                var attribute = new DiagnoseaAfterNowAttribute();
                
                // Act
                var result = attribute.IsValid(DateTime.UtcNow.AddSeconds(20));
                
                // Assert
                Assert.That(result, Is.True);
            }
        }
        
        public class FormatErrorMessage : DiagnoseaAfterNowAttributeTests
        {
            [Test]
            public void GivenErrorMessageIsAlreadySet_ReturnsAlreadySetErrorMessage()
            {
                // Arrange
                var attribute = new DiagnoseaAfterNowAttribute {ErrorMessage = "This is an error message"};
                
                // Act
                var result = attribute.FormatErrorMessage("name");
                
                // Assert
                Assert.That(result, Is.EqualTo(attribute.ErrorMessage));
            }

            [Test]
            public void GivenErrorMessageIsntAlreadySet_ReturnsInterchangeExceptionMessage()
            {
                // Arrange
                var attribute = new DiagnoseaAfterNowAttribute();
                
                // Act
                var result = attribute.FormatErrorMessage("name");
                
                // Assert
                Assert.That(result, Is.EqualTo(InterchangeExceptionMessages.InvalidDateAfterNow));
            }
        }
    }
}