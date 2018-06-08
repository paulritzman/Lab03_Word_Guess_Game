using System;

namespace WordGuessGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WelcomeMessage();

            string mainMenuInput = "";
            uint mainSelection = 0;
            while(mainSelection == 0)
            {
                MainMenu();
                mainMenuInput = Console.ReadLine();
                mainSelection = VerifyMainMenuInput(mainMenuInput);
            }

            int count = 0; // Protect against infinite loops while testing **remove after solved**
            while (mainSelection != 3 && count < 10)
            {
                switch (mainSelection)
                {
                    case 1:
                        Console.WriteLine("You hit 1!");
                        break;
                    case 2:
                        Console.WriteLine("You hit 2!");
                        break;
                    case 3:
                        Console.WriteLine("Bye!");
                        break;
                }

                count++;
            }

            Console.ReadLine();
        }
        
        // End of Main

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
                Console.Clear();
                return numericInput;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Sorry, that was not a valid menu option.\n");
                return 0;
            }
        }
    }
}
