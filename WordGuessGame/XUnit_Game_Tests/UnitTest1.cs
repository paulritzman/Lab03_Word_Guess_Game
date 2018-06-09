using System;
using WordGuessGame;
using Xunit;

namespace XUnit_Game_Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        public void CanVerifyMainMenuInput_Valid(string userInput)
        {
            Assert.True(Program.checkMainMenuInput(userInput));
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("123")]
        [InlineData("abc")]
        public void CanVerifyMainMenuInput_Invalid(string userInput)
        {
            Assert.False(Program.checkMainMenuInput(userInput));
        }

    }
}
