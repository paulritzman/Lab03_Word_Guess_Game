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
        public void CanCheckMainMenuInput_Valid(string userInput)
        {
            Assert.True(Program.CheckMainMenuInput(userInput));
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("123")]
        [InlineData("abc")]
        public void CanCheckMainMenuInput_Invalid(string userInput)
        {
            Assert.False(Program.CheckMainMenuInput(userInput));
        }

        [Fact]
        public void CanCreateWordBank()
        {
            Assert.True(Program.CreateWordBank());
        }

        [Fact]
        public void CanResetWordBank()
        {
            Assert.True(Program.ResetWordBank());
        }

        [Fact]
        public void CanViewWordBank()
        {
            Program.ResetWordBank();
            string[] allWords = new string[]
                        {
                            "donut",
                            "camel",
                            "Zimbabwe",
                            "Microsoft",
                            "carefully",
                            "programmatically",
                            "language",
                            "Honorificabilitudinitatibus",
                            "floccinaucinihilipilification",
                            "Llanfairpwllgwyngyllgogerychwyrndrobwllllantysiliogogogoch"
                        };

            foreach (string word in allWords)
            {
                Assert.Contains(word, Program.ViewWordBank());
            }
        }

        [Theory]
        [InlineData("abcd")]
        [InlineData("AbCd")]
        [InlineData("Seattle")]
        public void CanUpdateWordBank_Valid(string userInput)
        {
            Program.ResetWordBank();
            Assert.True(Program.UpdateWordBank(userInput));
        }

        [Theory]
        [InlineData("")]
        [InlineData("1234")]
        [InlineData("4bCd")]
        [InlineData("Sea ttle")]
        public void CanUpdateWordBank_Invalid(string userInput)
        {
            Program.ResetWordBank();
            Assert.False(Program.UpdateWordBank(userInput));
        }


    }
}
