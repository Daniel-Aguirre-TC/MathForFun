using System;
using System.Collections.Generic;
using System.Text;

namespace MathForFun
{
    public static class ConsoleHandler
    {
        static int consoleHalfWidth = Console.WindowWidth / 2;

        // string to store player names
        public static string playerOneName = "";
        public static string playerTwoName = "";
        
        public static string currentOperator;

        public static int playerOneScore;
        public static int playerTwoScore;


        static void WriteQuestion(int turnCounter)
        {
            // if is playerOne's turn
            if(turnCounter % 2 != 0)
            {
                string[] lineOne = new string[]
                {
                    $"{playerOneName}, what is", $"{playerTwoName} please wait."
                };
                string[] lineTwo = new string[]
                {
                    $"{GameManager.numOne} {currentOperator} {GameManager.numTwo} = ?", $"It is currently {playerOneName}'s turn."
                };
                PrintTwoStringsSplitScreen(lineOne[0], lineOne[1]);
                PrintScreenDivider(2);
                PrintTwoStringsSplitScreen(lineTwo[0], lineTwo[1]);
            }
            else
            // if it's player two's turn
            {
                string[] lineOne = new string[]
               {
                    $" {playerOneName} please wait.", $"{playerTwoName}, what is"
               };
                string[] lineTwo = new string[]
                {
                    $"It is currently {playerTwoName}'s turn.", $"{GameManager.numOne} {currentOperator} {GameManager.numTwo} = ?"
                };
                PrintTwoStringsSplitScreen(lineOne[0], lineOne[1]);
                PrintScreenDivider(2);
                PrintTwoStringsSplitScreen(lineTwo[0], lineTwo[1]);
            }

        }

        // Print my awesome Logo
        public static void ShowLogo()
        {            
            Console.Clear();          
            string[] red = new string[]
            {
                " _____    ______   ____  ",
                "|    |    |       |    \\ ",
                "|    |    |       |     \\",
               "------    |-----  |     |",
               "|     \\   |       |     |",
               "|      \\  |_____  |_____|"
            };
            string[] rain = new string[]
            {
                "______    ______   _____   _     ",
                "|    |    |     |    |    | \\   |",
                "|    |    |     |    |    |  \\  |",
                "------    |-----|    |    |   \\ |",
                "|     \\   |     |    |    |    \\|",
                "|      \\  |     |  __|__  |     |"
        };
            Console.WriteLine("\n\n\n\n");
            CenterAndPrintArray(red);
            Console.WriteLine("\n\n");
            CenterAndPrintArray(rain);
            Console.WriteLine("\n\n\n");
            Console.WriteLine(CenterText("Created by Daniel Aguirre"));
            ClearAfterReadKey();
        }
        // Introduction message with gameplay rules, after key press will Clear and OfferChangeSettings
        public static void GreetPlayer()
        {
            string[] greeting = new string[]
            {
                "Thanks for playing Math For Fun!\n",
                "Created by Daniel Aguirre.\n",
                "The rules are simple, I'll provide you with random math problems and you try to answer\n",
                "as many as you can correctly. The difficulty will increase as your score increases.\n",
                "If you get an answer wrong you can continue playing but your score will reset.\n",
                "Your score will be determined by your difficulty and the problem presented.\n",
                "Please press any key to continue."
            };
            CenterMidScreenAndPrintArray(greeting);
            ClearAfterReadKey();
            //OfferChangeSettings();
        }

        public static void DrawSinglePlayer()
        {
            Console.Clear();
            CenterMidScreenAndPrintArray(new string[] { 
                $" {playerOneName}'s Score: {ConsoleHandler.playerOneScore}\n", 
                "What is", 
                $"{GameManager.numOne} {currentOperator} {GameManager.numTwo} = ?"
            });
        }

        public static void DrawSplitScreen()
        {
            var playerScores = new string[]
            {
                $"{playerOneName}'s Score: {playerOneScore}",
                $"{playerTwoName}'s Score: {playerTwoScore}",
            };
            Console.Clear();
            Console.WriteLine("____________________________________________________________________________________________________");
            PrintScreenDivider(3);
            PrintTwoStringsSplitScreen(playerScores[0], playerScores[1]);
            PrintScreenDivider(3);
            WriteQuestion(GameManager.turnCount);
            PrintScreenDivider(6);


        }

        // Display a screen stating the player was correct when the right answer is entered.
        public static void PrintCorrectAnswerReceived(string playerName)
        {
            Console.Clear();
            ConsoleHandler.CenterMidScreenAndPrintArray(new string[] { 
                    $"Great job, {playerName}!",
                    $"{GameManager.numOne} {ConsoleHandler.currentOperator} {GameManager.numTwo} = {GameManager.currentAnswer} is correct!"
                    });
            ConsoleHandler.ClearAfterReadKey();

        }
        

        static void PrintTwoStringsSplitScreen(string leftString, string rightString)
        {
            Console.Write((leftString.PadLeft((consoleHalfWidth/2) + (leftString.Length / 2)).PadRight(consoleHalfWidth - 1)+ "|"));
            Console.WriteLine(rightString.PadLeft((consoleHalfWidth / 2) + (rightString.Length / 2)));
            //Console.Write();
        }


        // return the provided string padded to the left based on screen width to center on the screen.
        public static string CenterText(string textToPrint)
        {
            return textToPrint.PadLeft((int)MathF.Round(consoleHalfWidth + (textToPrint.Length / 2)));
        }    

        // Print a provided array of strings centered in the middle of the screen.
        public static void CenterMidScreenAndPrintArray(string[] stringsToPrint)
        {
            for (int i = 0; i < (Console.WindowHeight / 2) - (stringsToPrint.Length); i++)
            {
                Console.WriteLine();
            }
            CenterAndPrintArray(stringsToPrint);
        }

        // Print an array of strings in the center of the current line.
        public static void CenterAndPrintArray(string[] stringArrayToPrint)
        {
            foreach (var textLine in stringArrayToPrint)
            {
                Console.WriteLine(CenterText(textLine));
            }
        }

        // print "|" in the middle of the screen.
        static void PrintScreenDivider(int numberOfLinesToPrint)
        {
            for (int i = 0; i < numberOfLinesToPrint; i++)
            {
                Console.WriteLine(" ".PadRight(consoleHalfWidth - 1) + "|");
            }
        }

        // used to clear the screen on keypress
        public static void ClearAfterReadKey()
        {
            Console.ReadKey();
            Console.Clear();
        }
        



    }
}
