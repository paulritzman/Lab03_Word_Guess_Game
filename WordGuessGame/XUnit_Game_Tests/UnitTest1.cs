using System;
using WordGuessGame;
using Xunit;

namespace XUnit_Game_Tests
{
    public class UnitTest1
    {
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

        [Fact]
        public void CanChooseRandomWord()
        {
            Program.ResetWordBank();
            string allWords =
                            "donut" +
                            "camel" +
                            "Zimbabwe" +
                            "Microsoft" +
                            "carefully" +
                            "programmatically" +
                            "language" +
                            "Honorificabilitudinitatibus" +
                            "floccinaucinihilipilification" +
                            "Llanfairpwllgwyngyllgogerychwyrndrobwllllantysiliogogogoch";

            Assert.Contains(Program.ChooseRandomWord(), allWords);
        }

        [Fact]
        public void CanDecideWinner_Lose()
        {
            Assert.False(Program.DecideWinner(0));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CanDecideWinner_Win(uint guessesLeft)
        {
            Assert.True(Program.DecideWinner(guessesLeft));
        }

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

        [Theory]
        [InlineData(1, "1")]
        [InlineData(2, "2")]
        [InlineData(3, "3")]
        [InlineData(4, "4")]
        public void CanValidateWordBankMenuInput_Valid(uint expectedOutput, string userInput)
        {
            Assert.Equal(expectedOutput, Program.ValidatedWordBankMenuSelection(userInput));
        }
        
        [Theory]
        [InlineData(0, "-1")]
        [InlineData(0, "123")]
        [InlineData(0, "abc")]
        public void CanValidateWordBankMenuInput_Invalid(uint expectedOutput, string userInput)
        {
            Assert.Equal(expectedOutput, Program.ValidatedWordBankMenuSelection(userInput));
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

        [Theory]
        [InlineData("a")]
        [InlineData("z")]
        [InlineData("A")]
        [InlineData("Z")]
        public void CanCheckGameInput_Valid(string userInput)
        {
            Assert.True(Program.CheckGameInput(userInput));
        }

        [Theory]
        [InlineData("")]
        [InlineData("az")]
        [InlineData("A Z")]
        [InlineData("1")]
        public void CanCheckGameInput_Invalid(string userInput)
        {
            Assert.False(Program.CheckGameInput(userInput));
        }
        
        [Theory]
        [InlineData("A", "A")]
        [InlineData("asdf", "asdf")]
        [InlineData("Hello", "Hello")]
        public void CanCompareStrings_Valid(string wordToMatch, string guessedWord)
        {
            Assert.True(Program.CompareStrings(wordToMatch, guessedWord));
        }

        [Theory]
        [InlineData("ABCD", "A")]
        [InlineData("asdf", "asd")]
        [InlineData("HelloWorld", "Hello")]
        public void CanCompareStrings_Invalid(string wordToMatch, string guessedWord)
        {
            Assert.False(Program.CompareStrings(wordToMatch, guessedWord));
        }
    }
}
