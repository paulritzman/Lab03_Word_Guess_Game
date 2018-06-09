using System;

namespace WordGuessGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WelcomeMessage();

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
                        Console.WriteLine("You hit 2!");
                        Environment.Exit(0);
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            } while (mainSelection != 3);

            Console.ReadLine();
        }
        
        // End of Main

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
    }
}
