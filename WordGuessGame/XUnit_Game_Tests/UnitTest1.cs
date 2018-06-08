using System;
using WordGuessGame;
using Xunit;

namespace XUnit_Game_Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(1, "1")]
        [InlineData(2, "2")]
        [InlineData(3, "3")]
        public void CanVerifyMainMenuInput_Any_Valid(uint expectedOutput, string userInput)
        {
            Assert.Equal(expectedOutput, Program.VerifyMainMenuInput(userInput));
        }

        [Theory]
        [InlineData(0, "-1")]
        [InlineData(0, "123")]
        [InlineData(0, "abc")]
        public void CanVerifyMainMenuInput_Any_Invalid(uint expectedOutput, string userInput)
        {
            Assert.Equal(expectedOutput, Program.VerifyMainMenuInput(userInput));
        }

    }
}
