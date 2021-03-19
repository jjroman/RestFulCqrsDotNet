using System;
using JJ.Common;
using JJ.Common.Dates;
using JJ.UserTechTest.Models;
using Xunit;

namespace JJ.UserTechTest.Tests
{
    public class UserTest
    {
        [Fact]
        public void ItShouldReturnTheSameAge()
        {
            // Arrange
            IDateTimeProvider fixedDateProvider = new FixedDateTimeProvider(new DateTime(2020, 1, 1));
            var user = User.CreateForGettingAge("Javier", new DateTime(2000, 1, 1));

            // Act
            var age = user.GetAge(fixedDateProvider);

            // Assert
            Assert.Equal(20, age);
        }
    }
}