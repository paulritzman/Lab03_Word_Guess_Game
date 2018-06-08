using System;

namespace WordGuessGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WelcomeMessage();

            MainMenu();

            Console.ReadLine();
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("\tWelcome to the \"Guess the Word!\"\n");
        }

        private static void MainMenu()
        {
            Console.WriteLine(
                "Please select one of the options below:\n" +
                "1) Play a Game!\n" +
                "2) Edit Word Bank\n" +
                "3) Exit\n");
        }

        public static uint VerifyMainMenuInput(string mainMenuInput)
        {
            bool isValid = uint.TryParse(mainMenuInput, out uint numericInput);
            
            if (isValid && (numericInput >= 1 && numericInput <= 3))
            {
                return numericInput;
            }

            return 0;
        }
    }
}
