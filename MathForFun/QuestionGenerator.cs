using System;
using System.Collections.Generic;
using System.Text;

namespace MathForFun
{
    public static class QuestionGenerator
    {
        // Variables //

        public enum QuestionType { None, Addition, Subtraction, Multiplication, Division }
        // char opperator is updated when DisplayQuestion() is called.
        public static char currentOperator;
        // numOne and numTwo for current problem
        public static int numOne = 0;
        public static int numTwo = 0;

        // List of randomly selected strings to display before presenting question.
        static List<string> questionText = new List<string>()
        {
            new string ("I bet you can't figure this one out!"),
            new string ("This one's a doozy!"),
            new string ("You're for real a genius if you can answer this."),
            new string ("If you can figure this one out, you can\nfigure anything out!"),
            new string ("There's no way you can answer this one right."),
            new string ("Fifty bucks says you get this one wrong! (jk)"),
            new string ("This one stumped Einstein, you think you\ncan out smart him?"),
            new string ("Here's a hard one!"),
            new string ("In the words of Admiral Ackbar, ITS A TRAP!"),
            new string ("Only someone with an IQ of a million can\nanswer this one correctly."),
            new string ("This one is nearly impossible"),
            new string ("Google told me you couldn't answer this\nwithout them"),
            new string ("Did you know you could use enter to\ncontinue after a question?"),
            new string ("This next one is really gunna stump you!"),
            new string ("Not even Chuck Norris could answer this one!"),
            new string ("I doubt you're gunna get this, but give it\nyour best shot!"),
            new string ("If you get this one right then I know that you\nyou're using a calculator"),
            new string ("Thanks for playing my game! - Daniel")
        };
        // unusedQuestionText is filled up with questionText List, then each time an option is used it's removed. When the list is empty it will re-fill then remove last used.
        static List<string> unusedQuestionText = new List<string>()
        {
            new string ("Just because this is the first question doesn't\nmean that it'll be easy!")
        };
        
        // Methods //

        // Return a new equation to GameManager based on the category passed, and the number ranges provided.
        
        
        /*
        static public int NewProblem(QuestionType category, int numOneMinValue, int numOneMaxValue, int numTwoMinValue, int numTwoMaxValue)
        {
            SetNumbers(category, numOneMinValue, numOneMaxValue, numTwoMinValue, numTwoMaxValue);
            DisplayProblem(category);
            switch(category)
            {
                case QuestionType.Addition:
                    return numOne + numTwo;
                case QuestionType.Subtraction:
                    return numOne - numTwo;
                case QuestionType.Multiplication:
                    return numOne * numTwo;
                case QuestionType.Division:
                    return numOne / numTwo;
                default:
                    Console.WriteLine("Error with returning CorrectAnswer from NewProblem() due to no matching QuestionType. Will Return 0.");
                    return 0;
            }   
        }
        // Takes info from NewProblem() and uses it to generate a problem.
        

        
        // Print the NewProblem() to the console screen
        static void DisplayProblem(QuestionType questionType)
        {
            Console.Clear();
            // Display a randomly selected string (other than first question) above math problem.
            DisplayRandomText();
            switch(questionType)
            {
                case QuestionType.Addition:
                    currentOperator = '+';
                    break;
                case QuestionType.Subtraction:
                    currentOperator = '-';
                    break;
                case QuestionType.Multiplication:
                    currentOperator = '*';
                    break;
                case QuestionType.Division:
                    currentOperator = '/';
                    break;
                default:
                    Console.WriteLine("Error with determining opperator. No valid QuestionType submitted for DisplayProblem(). Returning +");
                    currentOperator = '+';
                    break;
            }
            
            Console.WriteLine($"\nWhat is {numOne} {currentOperator} {numTwo} = ?\n");
        }
        // Print random text to the console above the math problem presented, just for fun!
        static void DisplayRandomText()
        {
            // get a random index in unusedQuestionText list
            int randomIndex = new Random().Next(0, unusedQuestionText.Count - 1);
            // writeLine for that index then remove from unusedQuestionText
            Console.WriteLine(unusedQuestionText[randomIndex]);
            unusedQuestionText.RemoveAt(randomIndex);
            // if unusedQuesetionText is now empty, refill with questionText list.
            if (unusedQuestionText.Count == 0)
            {
                foreach(string text in questionText)
                {
                    unusedQuestionText.Add(text);
                }
            }
        }
        // store the LastNums from previous problem to avoid duplicate equations.

        */
    }
}
