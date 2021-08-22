using System;
using System.Collections.Generic;
using System.Text;

namespace MathForFun
{
    public static class QuestionGenerator
    {
        // numOne and numTwo for current problem
        public static int numOne = 0;
        public static int numTwo = 0;
        // lastNumOne and lastNumTwo used to prevent showing the same answer twice.
        static int lastNumOne;
        static int lastNumTwo;
        // List of randomly selected strings to display before presenting question.
        static List<string> questionText = new List<string>()
        {
            new string ("I bet you can't figure this one out!"),
            new string ("This one's a doozy!"),
            new string ("You're for real a genius if you can answer this."),
            new string ("There's no way you can answer this one right."),
            new string ("Fifty bucks says you get this one wrong! (jk)"),
            new string ("Here's a hard one!"),
            new string ("Google told me you couldn't answer this\nwithout them"),
            new string ("Did you know you could use enter to\ncontinue after a question?"),
            new string ("This next one is really gunna stump you!"),
            new string ("Not even Chuck Norris could answer this one!"),
            new string ("I doubt you're gunna get this, but give\nit your best shot!"),
            new string ("If you get this one right then I know\nyou're using a calculator"),
            new string ("Thanks for playing my game! - Daniel")
        };

        static public int NewProblem(int numOneMinValue, int numOneMaxValue, int numTwoMinValue, int numTwoMaxValue)
        {
            SetNumbers(numOneMinValue, numOneMaxValue, numTwoMinValue, numTwoMaxValue);
            DisplayProblem();
            return numOne + numTwo;       
        }

        static void DisplayProblem()
        {
            Console.Clear();
            var random = new Random();
            Console.WriteLine(questionText[random.Next(0, questionText.Count - 1)]);
            Console.WriteLine($"\nWhat is {numOne} + {numTwo} = ?\n");
        }

        static void SetNumbers(int firstNumberMin, int firstNumberMax, int secondNumberMin, int secondNumberMax)
        {
            var random = new Random();
            numOne = random.Next(firstNumberMin, firstNumberMax);
            numTwo = random.Next(secondNumberMin, secondNumberMax);
            if ((numOne == lastNumOne && numTwo == lastNumTwo) || (numOne == lastNumTwo && numTwo == lastNumOne))
            {
                SetNumbers(firstNumberMin, firstNumberMax, secondNumberMin, secondNumberMax);
            }
            else
            {
                lastNumOne = numOne;
                lastNumTwo = numTwo;
            }
        }

    }
}
