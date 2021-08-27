using System;
using System.Collections.Generic;
using System.Text;

namespace MathForFun
{
    public static class ConsoleHandler
    {


        // string to store player names
        public static string playerOneName = "";
        public static string playerTwoName = "";



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

        public static void CenterMidScreenAndPrintArray(string[] stringsToPrint)
        {
            for (int i = 0; i < (Console.WindowHeight / 2) - (stringsToPrint.Length); i++)
            {
                Console.WriteLine();
            }
            CenterAndPrintArray(stringsToPrint);
        }

        public static void DrawSplitScreen()
        {
            Console.WriteLine("____________________________________________________________________________________________________");
            Console.WriteLine(" ".PadRight(49) + "|");
            Console.WriteLine(" ".PadRight(49) + "|");
            Console.WriteLine();

        }




        // return the provided string padded to the left based on screen width to center on the screen.
        public static string CenterText(string textToPrint)
        {
            return textToPrint.PadLeft((int)MathF.Round((Console.WindowWidth / 2) + (textToPrint.Length / 2)));
        }

        // take a provided array of strings and print it centered
        public static void CenterAndPrintArray(string[] stringArrayToPrint)
        {
            foreach (var textLine in stringArrayToPrint)
            {
                Console.WriteLine(CenterText(textLine));
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
