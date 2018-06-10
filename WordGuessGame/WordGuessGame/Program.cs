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
                mainSelection = ValidatedMainMenuInput();

                switch (mainSelection)
                {
                    case 1:
                        Console.WriteLine("You hit 1!");
                        Environment.Exit(0);
                        break;
                    case 2:
                        EditWordBank();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            } while (mainSelection != 3);

            Console.ReadLine();
        }

        // End of Main

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
                        sw.WriteLine("Seattle");
                        sw.WriteLine("Honorificabilitudinitatibus");
                        sw.WriteLine("Llanfairpwllgwyngyllgogerychwyrndrobwllllantysiliogogogoch");
                    };
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        $"An error occurred: {e.Message} While trying to create the word bank.\n" +
                        $"Please try restarting the program.");
                }
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
                    $"An error occurred: {e.Message} While trying to reset the word bank.\n" +
                    $"Please try resetting it again.");
            }
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("\tWelcome to the \"Guess the Word!\"\n");
        }

        public static uint ValidatedMainMenuInput()
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
                    Console.WriteLine("Sorry, that number didn't match one of the menu options.\n");
                    return false;
                }
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Sorry, that didn't match one of the menu options. Please try again.\n");
                return false;
            }
        }

        private static void EditWordBank()
        {
            Console.Clear();

            string editMenuInput = "";
            uint optionSelected = 0;

            do
            {
                PrintEditMenu();

                editMenuInput = Console.ReadLine();
                optionSelected = ValidatedEditMenuInput(editMenuInput);

                switch (optionSelected)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Viewing words...");
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Adding words...");
                        break;
                    case 3:
                        Console.Clear();
                        ResetWordBank();
                        break;
                    case 4:
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Sorry, that didn't match one of the menu options.\n");
                        break;
                }
            } while (optionSelected != 4);
        }

        private static void PrintEditMenu()
        {
            Console.WriteLine(
                "Select an option from the menu below to manage the word bank for the guessing game:\n" +
                "1) View words in word bank\n" +
                "2) Add a word to the word bank\n" +
                "3) Reset the word bank to the default words\n" +
                "4) Return to main menu\n");
        }

        public static uint ValidatedEditMenuInput(string userInput)
        {
            bool isValid = uint.TryParse(userInput, out uint menuOptionSelected);

            if (isValid && (menuOptionSelected >= 1 && menuOptionSelected <= 4))
            {
                return menuOptionSelected;
            }
            else
            {
                return 0;
            }
        }


    }
}
