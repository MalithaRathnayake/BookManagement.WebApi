using BookManagement.Domain.Validators;
using FluentAssertions; 

namespace BookManagement.UnitTests
{
    public class DateInPastAttributeTests
    {  
        [Fact]
        public void IsValid_DateInPast_ReturnsSuccess()
        {
            // Arrange
            var attribute = new DateInPastAttribute();
            var pastDate = DateTime.Now.AddDays(-1);

            // Act
            var result = attribute.IsValid(pastDate);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsValid_DateInFuture_ReturnsValidationError()
        {
            // Arrange
            var attribute = new DateInPastAttribute();
            var futureDate = DateTime.Now.AddDays(1);

            // Act
            var result = attribute.IsValid(futureDate);

            // Assert
            result.Should().BeFalse(); 
        }

        [Fact]
        public void IsValid_NullDate_ReturnsSuccess()
        {
            // Arrange
            var attribute = new DateInPastAttribute();
            DateTime? nullDate = null;

            // Act
            var result = attribute.IsValid(nullDate);

            // Assert
            result.Should().BeTrue();
        }

    }
}
