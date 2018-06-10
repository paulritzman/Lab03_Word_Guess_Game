using System;
using System.IO;

namespace WordGuessGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WelcomeMessage();
            CreateWordBank();

            uint mainSelection = 0;
            do
            {
                mainSelection = ValidatedMainMenuSelection();

                switch (mainSelection)
                {
                    case 1:
                        PlayGame();
                        break;
                    case 2:
                        WordBankMenu();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            } while (mainSelection != 3);

            Console.ReadLine();
        }

        // End of Main

        private static void WordBankMenu()
        {
            Console.Clear();

            string wordBankMenuInput = "";
            string allWords = "";
            uint optionSelected = 0;

            do
            {
                PrintWordBankMenu();

                wordBankMenuInput = Console.ReadLine();
                optionSelected = ValidatedWordBankMenuSelection(wordBankMenuInput);

                Console.Clear();

                switch (optionSelected)
                {
                    case 1:
                        allWords = ViewWordBank();
                        if (allWords.Length > 0)
                        {
                            Console.WriteLine(allWords);
                        }
                        break;
                    case 2:
                        UpdateWordBank();
                        break;
                    case 3:
                        ResetWordBank();
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Sorry, that didn't match any of the menu options.\n");
                        break;
                }
            } while (optionSelected != 4);
        }

        private static void CreateWordBank()
        {
            string filePath = "../../../../../wordbank.txt";
            if (!File.Exists(filePath))
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        sw.WriteLine("donut");
                        sw.WriteLine("camel");
                        sw.WriteLine("Zimbabwe");
                        sw.WriteLine("Microsoft");
                        sw.WriteLine("carefully");
                        sw.WriteLine("programmatically");
                        sw.WriteLine("language");
                        sw.WriteLine("Honorificabilitudinitatibus");
                        sw.WriteLine("floccinaucinihilipilification");
                        sw.WriteLine("Llanfairpwllgwyngyllgogerychwyrndrobwllllantysiliogogogoch");
                    };
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        $"Unable to create word bank: {e.Message}\n");
                }
            }
        }

        private static string ViewWordBank()
        {
            string filePath = "../../../../../wordbank.txt";

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string wordBank = sr.ReadToEnd();
                    return wordBank;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"The word bank could not be read: {e.Message}\n");
                return "";
            }
        }

        private static void UpdateWordBank()
        {
            string filePath = "../../../../../wordbank.txt";

            Console.WriteLine("What would you like to add?\n");
            string newWord = Console.ReadLine();
            bool newWordIsValid = checkUpdateWordBankInput(newWord);

            if (newWordIsValid)
            {
                try
                {
                    using (StreamWriter sw = File.AppendText(filePath))
                    {
                        sw.WriteLine(newWord);

                        Console.Clear();
                        Console.WriteLine("Your word has been added to the word bank.\n");
                    }
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine($"Unable to add a word at this time: {e.Message}\n");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Unable to add word, must only contain letters.\n");
            }
        }

        public static bool checkUpdateWordBankInput(string inputWord)
        {
            if (inputWord.Length == 0)
            {
                return false;
            }

            try
            {
                foreach (char c in inputWord.ToCharArray())
                {
                    if (!(c >= 65 && c <= 90) && !(c >= 97 && c <= 122))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to add word to word bank: {e.Message}");
                return false;
            }
        }

        public static void ResetWordBank()
        {
            string filePath = "../../../../../wordbank.txt";

            try
            {
                File.Delete(filePath);
                CreateWordBank();

                Console.WriteLine("Word bank reset!\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    $"The word bank could not be reset: {e.Message}\n");
            }
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("\tWelcome to the \"Guess the Word!\"\n");
        }

        public static uint ValidatedMainMenuSelection()
        {
            string mainMenuInput = "";
            bool isValid = false;

            while (isValid == false)
            {
                PrintMainMenu();
                mainMenuInput = Console.ReadLine();

                isValid = checkMainMenuInput(mainMenuInput);
            }

            return uint.Parse(mainMenuInput);
        }

        private static void PrintMainMenu()
        {
            Console.WriteLine(
                "Please select one of the options below:\n" +
                "1) Play a Game!\n" +
                "2) Edit Word Bank\n" +
                "3) Exit\n");
        }

        public static bool checkMainMenuInput(string userInput)
        {
            uint inputNum = 0;
            try
            {
                inputNum = uint.Parse(userInput);

                if (inputNum >= 1 && inputNum <= 3)
                {
                    return true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Sorry, that number didn't match any of the menu options.\n");
                    return false;
                }
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Sorry, that didn't match any of the menu options. Please try again.\n");
                return false;
            }
        }

        private static void PrintWordBankMenu()
        {
            Console.WriteLine(
                "Select an option from the menu below to manage the word bank for the guessing game:\n" +
                "1) View words in word bank\n" +
                "2) Add a word to the word bank\n" +
                "3) Reset the word bank to the default words\n" +
                "4) Return to main menu\n");
        }

        public static uint ValidatedWordBankMenuSelection(string wordBankMenuInput)
        {
            bool isValid = uint.TryParse(wordBankMenuInput, out uint menuOptionSelected);

            if (isValid && (menuOptionSelected >= 1 && menuOptionSelected <= 4))
            {
                return menuOptionSelected;
            }
            else
            {
                return 0;
            }
        }

        public static void PlayGame()
        {
            Console.Clear();

            string randomWord = ChooseRandomWord();
            string userGuess = "", allGuesses = "", guessedWord = "";

            uint incorrectGuessesLeft = 3;

            do
            {
                Console.WriteLine(randomWord);

                foreach (char c in randomWord.ToLower())
                {
                    if (!allGuesses.ToLower().Contains(c))
                    {
                        Console.Write("_ ");
                    }
                    else
                    {
                        Console.Write($"{c} ");
                    }
                }

                Console.WriteLine(allGuesses);
                Console.WriteLine($"Guesses Left: {incorrectGuessesLeft}");
                Console.WriteLine();
                
                Console.WriteLine("Guess a letter:\n");
                userGuess = Console.ReadLine();

                Console.Clear();

                bool guessIsValid = checkGameInput(userGuess);

                if (guessIsValid && !allGuesses.Contains(userGuess))
                {
                    allGuesses += userGuess;

                    if (randomWord.Contains(userGuess))
                    {
                        guessedWord += userGuess;
                    }
                    else
                    {
                        incorrectGuessesLeft--;
                    }
                }
                else if (allGuesses.Contains(userGuess) && userGuess.Length > 0)
                {
                    Console.WriteLine("You already guessed that letter.\n");
                }
                else
                {
                    Console.WriteLine("Guesses must consist of a single letter.\n");
                    incorrectGuessesLeft--;
                }
            } while (guessedWord.Length != randomWord.Length && incorrectGuessesLeft > 0);

            Console.Clear();
            DecideWinner(incorrectGuessesLeft);
        }

        public static string ChooseRandomWord()
        {
            string filePath = "../../../../../wordbank.txt";
            Random randomLineNumber = new Random();

            try
            {
                string[] wordArray = File.ReadAllLines(filePath);

                return wordArray[randomLineNumber.Next(1, wordArray.Length)];
            }
            catch (Exception e)
            {
                Console.WriteLine($"The word bank could not be read: {e.Message}\n");
                return "";
            }
        }

        public static bool checkGameInput(string userGuess)
        {
            if (userGuess.Length != 1)
            {
                return false;
            }

            try
            {
                foreach (char c in userGuess.ToCharArray())
                {
                    if (!(c >= 65 && c <= 90) && !(c >= 97 && c <= 122))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to process your guess: {e.Message}");
                return false;
            }
        }

        public static void DecideWinner(uint incorrectGuessesLeft)
        {
            if (incorrectGuessesLeft > 0)
            {
                Console.WriteLine("You won!\n");
            }
            else
            {
                Console.WriteLine("You lose...\n");
            }
        }
    }
}
