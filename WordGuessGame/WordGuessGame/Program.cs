using System;
using System.IO;

namespace WordGuessGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Print guessing game greeting, and create the word bank text file.
            WelcomeMessage();
            CreateWordBank();

            // Loop until user selects option 3 to exit the application
            uint mainSelection = 0;
            do
            {
                // Check if the user inputs a valid menu option.
                mainSelection = ValidatedMainMenuSelection();

                switch (mainSelection)
                {
                    case 1: // Executes helper method to start the guessing game
                        PlayGame();
                        break;
                    case 2: // Executes helper method to display menu for user to manage the word bank text file.
                        WordBankMenu();
                        break;
                    case 3: // Exits the application
                        Environment.Exit(0);
                        break;
                }
            } while (mainSelection != 3);
        }

        /// <summary>
        /// Helper method to show the word bank menu options, looping until the user exits the menu
        /// </summary>
        private static void WordBankMenu()
        {
            // Clear the console window.
            Console.Clear();

            string wordBankMenuInput = "", allWords = "";
            uint optionSelected = 0;

            // Loop until the user selects option 4 to return to the main menu.
            do
            {
                // Display the word bank menu.
                PrintWordBankMenu();

                // Accept user input for the menu desired menu option. Validate that the input matches an option from the menu.
                wordBankMenuInput = Console.ReadLine();
                optionSelected = ValidatedWordBankMenuSelection(wordBankMenuInput);

                // Clear the word bank menu.
                Console.Clear();

                switch (optionSelected)
                {
                    case 1: // Display all words from the word bank text file.
                        allWords = ViewWordBank();
                        if (allWords.Length > 0)
                        {
                            Console.WriteLine(allWords);
                        }
                        break;
                    case 2: // Prompt user to add a word to the word bank text file.
                        Console.WriteLine("What would you like to add?\n");
                        string newWord = Console.ReadLine();
                        UpdateWordBank(newWord);
                        break;
                    case 3: // Reset the word bank text file to the default words.
                        ResetWordBank();
                        break;
                    case 4: // Exits from the word bank menu, return user to the main menu.
                        break;
                    default: // Prints message if user did not enter a valid menu option.
                        Console.WriteLine("Sorry, that didn't match any of the menu options.\n");
                        break;
                }
            } while (optionSelected != 4);
        }

        /// <summary>
        /// Method which creates a file storing the words used in the guessing game. Populates file with initial words.
        /// </summary>
        /// <returns>Boolean: file creation success/failure</returns>
        public static bool CreateWordBank()
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
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unable to create word bank: {e.Message}\n");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Method which returns each word from the word bank text file.
        /// </summary>
        /// <returns>String: list of all words from word bank text file</returns>
        public static string ViewWordBank()
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

        /// <summary>
        /// Method which calls CheckUpdateWordBankInput(), passing in the input word.
        /// If CheckUpdateWordBankInput() returns true, the input word is appended to the word bank text file.
        /// </summary>
        /// <param name="newWord">String the user wants to add to the word bank text file.</param>
        /// <returns>Boolean: success/failure in appending the input word to the word bank text file</returns>
        public static bool UpdateWordBank(string newWord)
        {
            string filePath = "../../../../../wordbank.txt";
            bool newWordIsValid = CheckUpdateWordBankInput(newWord);

            if (newWordIsValid)
            {
                try
                {
                    using (StreamWriter sw = File.AppendText(filePath))
                    {
                        sw.WriteLine(newWord);

                        Console.Clear();
                        Console.WriteLine("Your word has been added to the word bank.\n");
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine($"Unable to add a word at this time: {e.Message}\n");
                    return false;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Unable to add word, must only contain letters.\n");
                return false;
            }
        }

        /// <summary>
        /// Method to test if each character in the string the user wants to add to the word bank text file
        /// is a letter from [a-z] or [A-Z].
        /// </summary>
        /// <param name="inputWord">String that the user wants to add to the word bank text file.</param>
        /// <returns>Boolean: input string consists of only letters from [a-z] or [A-Z]</returns>
        public static bool CheckUpdateWordBankInput(string inputWord)
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

        /// <summary>
        /// Method which deletes the word bank text file, then calls CreatWordBank() to recreate the file with only the default words present.
        /// </summary>
        /// <returns>Boolean: success/failure of word bank text file deletion/recreation</returns>
        public static bool ResetWordBank()
        {
            string filePath = "../../../../../wordbank.txt";

            try
            {
                File.Delete(filePath);
                CreateWordBank();

                Console.WriteLine("Word bank reset!\n");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"The word bank could not be reset: {e.Message}\n");
                return false;
            }
        }

        /// <summary>
        /// Method to print greeting to the screen upon program execution.
        /// </summary>
        private static void WelcomeMessage()
        {
            Console.WriteLine("\tWelcome to the \"Guess the Word!\"\n");
        }

        /// <summary>
        /// Helper method which prints the main menu to the console window, 
        /// looping until the user inputs a valid menu option (calls CheckMainMenuInput() to test validity).
        /// </summary>
        /// <returns>Uint: menu option selected by the user</returns>
        public static uint ValidatedMainMenuSelection()
        {
            string mainMenuInput = "";
            bool isValid = false;

            while (isValid == false)
            {
                PrintMainMenu();
                mainMenuInput = Console.ReadLine();

                isValid = CheckMainMenuInput(mainMenuInput);
            }

            return uint.Parse(mainMenuInput);
        }

        /// <summary>
        /// Method which prints the the main menu to the screen.
        /// </summary>
        private static void PrintMainMenu()
        {
            Console.WriteLine(
                "Please select one of the options below:\n" +
                "1) Play a Game!\n" +
                "2) Edit Word Bank\n" +
                "3) Exit\n");
        }

        /// <summary>
        /// Method to check if the user input on the main menu matches any of the menu options.
        /// </summary>
        /// <param name="userInput">Menu option selected by the user.</param>
        /// <returns>Boolean: user input menu selection matches an existing menu option</returns>
        public static bool CheckMainMenuInput(string userInput)
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

        /// <summary>
        /// Method which prints the word bank menu to the screen.
        /// </summary>
        private static void PrintWordBankMenu()
        {
            Console.WriteLine(
                "Select an option from the menu below to manage the word bank for the guessing game:\n" +
                "1) View words in word bank\n" +
                "2) Add a word to the word bank\n" +
                "3) Reset the word bank to the default words\n" +
                "4) Return to main menu\n");
        }

        /// <summary>
        /// Method which tests the if the user input menu selection matches any of the menu options.
        /// </summary>
        /// <param name="wordBankMenuInput">Menu option sleected by the user.</param>
        /// <returns>Uint: menu option selected by the user; 0 is returned if user input does not match a menu option</returns>
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

        /// <summary>
        /// Help method which runs the guessing game, looping until the word is guessed or there are no guess chances are left.
        /// </summary>
        public static void PlayGame()
        {
            // Clear the console window.
            Console.Clear();

            // Instantiate variables used in the guessing game.
            string userGuess = "", allGuesses = "", guessedWord = "";
            bool guessIsValid = false, doStringsMatch = false;
            uint incorrectGuessesLeft = 3; // Sets the number of incorrect guesses the user gets before losing the game.

            // Select a random word from the word bank text file.
            string randomWord = ChooseRandomWord();

            // Loop until the random word is guessed or the number of incorrect guesses left reaches 0. 
            do
            {
                // Print underscores for each letter in the random word, so user knows how many letters to guess.
                // Updates to show correctly guessed letters alongside underscores of letters still needing to be guessed.
                foreach (char c in randomWord)
                {
                    if (!allGuesses.ToLower().Contains(char.ToLower(c)))
                    {
                        Console.Write("_ ");
                    }
                    else
                    {
                        Console.Write($"{c} ");
                    }
                }

                // Displays to the user all letters which have been guessed, and the number of incorrect guesses left.
                Console.WriteLine($"Guessed letters: {allGuesses.ToLower()}");
                Console.WriteLine($"Guesses Left: {incorrectGuessesLeft}");
                Console.WriteLine();
                
                // Prompt the user to guess a letter in the random word.
                Console.Write("Guess a letter: ");
                userGuess = Console.ReadLine();

                // Clear the console window.
                Console.Clear();

                // Checks if the value the user input as a guess is a single letter between [a-z] or [A-Z].
                guessIsValid = CheckGameInput(userGuess);

                // Checks if the input letter hasn't been guessed by the user yet
                if (guessIsValid && !allGuesses.Contains(userGuess))
                {
                    // Adds the input letter to the list of all letters that have been guessed.
                    allGuesses += userGuess;

                    // Check if the input letter appears within the random word.
                    if (randomWord.ToLower().Contains(userGuess.ToLower()))
                    {
                        // Add the input letter to the list of guessed letters which appear in the random word for later string comparison.
                        guessedWord += userGuess;
                    }
                    else
                    {
                        // Decrement the number of incorrect guesses the user has left if the guessed letter does not appear in the random word.
                        incorrectGuessesLeft--;
                    }
                }
                // Checks if the user has already guessed the input letter.
                else if (allGuesses.Contains(userGuess) && userGuess.Length > 0)
                {
                    Console.WriteLine("You already guessed that letter.\n");
                }
                // Handle user input that does not consist of a single letter.
                else
                {
                    Console.WriteLine("Guesses must consist of a single letter.\n");
                    incorrectGuessesLeft--;
                }

                // Compare the letters in the random word to each of the letters the user has guessed which appear in the random word
                // Evaluates to true if all letters in the random word also appear in the guessed letters.
                doStringsMatch = CompareStrings(randomWord, guessedWord);
            } while (doStringsMatch == false && incorrectGuessesLeft > 0);

            // Clear the console window.
            Console.Clear();

            // Determine if the above loop exited due to the random word being guessed or due to the user having no incorrect guess chances left.
            bool isWinner = DecideWinner(incorrectGuessesLeft);
            if (isWinner)
            {
                Console.WriteLine("You won!\n");
            }
            else
            {
                Console.WriteLine("You lose...\n");
            }
        }

        /// <summary>
        /// Method which selects a random word from the word bank text file.
        /// </summary>
        /// <returns>String: random word from the word bank text file</returns>
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

        /// <summary>
        /// Method which checks if the user guess is a char from [a-z] or [A-Z].
        /// </summary>
        /// <param name="userGuess">Guessed letter in the random word from the word bank text file.</param>
        /// <returns>Boolean: char entered by user is between [a-z] or [A-Z]</returns>
        public static bool CheckGameInput(string userGuess)
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

        /// <summary>
        /// Method which tests if the each letter in the random word from the word bank text file has been guessed by the user.
        /// </summary>
        /// <param name="randomWord">Random word from the word bank text file.</param>
        /// <param name="guessedWord">String of letters guessed by the user which also appear in the random word.</param>
        /// <returns>Boolean: all letters in the random word also appear in the guessed word</returns>
        public static bool CompareStrings(string randomWord, string guessedWord)
        {
            string wordToGuess = randomWord.ToLower();
            string guesses = guessedWord.ToLower();

            foreach (char c in wordToGuess)
            {
                if (!guesses.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Method to determine if there are any incorrect guess chances left at end of game.
        /// </summary>
        /// <param name="incorrectGuessesLeft">Number of incorrect guess chances left.</param>
        /// <returns>Boolean: incorrect guess chances left greater than or equal to 0</returns>
        public static bool DecideWinner(uint incorrectGuessesLeft)
        {
            if (incorrectGuessesLeft > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
