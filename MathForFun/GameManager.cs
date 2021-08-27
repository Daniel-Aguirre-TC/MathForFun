using System;
using System.Collections.Generic;
using System.Text;

namespace MathForFun
{

    public static class GameManager
    {
        // Variables //

        // numberOfPlayers will adjust the screen layout and the gameplay.
        public enum NumberOfPlayers { None, One, Two};
        public static NumberOfPlayers playerCount = NumberOfPlayers.None;

        // difficulty will adjust how often the number range increases, and by how much. Score is also dependent on difficulty.
        public enum Difficulty {None, Easy, Medium, Hard };
        // setting difficulty and questionType to none until player selects a difficulty/type.
        public static Difficulty currentDifficulty = Difficulty.None;
        public static QuestionGenerator.QuestionType questionType = QuestionGenerator.QuestionType.None;
        // stillPlaying is used to keep while loop running for gameplay
        public static bool stillPlaying;
        // numbers below are starting point for min/max values of opperands. These are increased throughout gameplay.
        public static int firstMinNumber;
        public static int firstMaxNumber;
        public static int secondMinNumber;
        public static int secondMaxNumber;
        // stores the correct answer for the current question
        public static int currentAnswer;
        // correctCount is used to display score. Increases by one per correct answer.
        public static int correctCount;
        // scoreBooster will increase the score the higher the sum of currentAnswer is. This allows you to get more
        // points for more difficult questions, not just the difficulty selected.
        public static int scoreBooster = 0;

        // Constructor//


        public static void StartGame()
        {
            // stillPlaying bool to keep gameplay loop running in while statement below.
            stillPlaying = true;
            ConsoleHandler.ShowLogo();
            ConsoleHandler.GreetPlayer();
            InputHandler.GetNumberOfPlayers();
            InputHandler.GetPlayerNames();
            InputHandler.GetMathCategory();
            InputHandler.OfferDifficulty();
            ConsoleHandler.DrawSplitScreen();
        }

        // GameManager Constructor is called in Main at startup. 
        //public GameManager()
        //{         
            
            
            // gameplay loop based on stillPlaying - this bool is changed by OfferContinue() method
            //while(stillPlaying)
            //{
                // generate new question and assign the answer to currentAnswer
           //     currentAnswer = QuestionGenerator.NewProblem(questionType, firstMinNumber, firstMaxNumber, secondMinNumber, secondMaxNumber);
           //     CheckAnswer();
           // }
       // }

        // Methods //



     
 
        static void IncreaseNumberRange()
        {
            // increase range based on difficulty
            switch (currentDifficulty)
            {
                // if easy increase number ranges as shown below every turn and every four turns.
                case Difficulty.Easy:
                    firstMaxNumber++;
                    if (correctCount % 4 == 0)
                    {
                        firstMinNumber++;
                        secondMinNumber++;
                        secondMaxNumber++;
                        if (questionType == QuestionGenerator.QuestionType.Division)
                        {
                            // increase max number by more on Division
                            firstMaxNumber += 5;
                            // undo increase to second min Number to avoid limiting division options.
                            secondMinNumber--;
                        }
                    }

                    break;
                // if medium increase number ranges as shown below every turn and every three turns
                case Difficulty.Medium:
                    firstMinNumber++;
                    firstMaxNumber++;
                    if (correctCount % 3 == 0)
                    {
                        firstMinNumber++;
                        firstMaxNumber++;
                        secondMinNumber += 2;
                        secondMaxNumber += 2;
                        if (questionType == QuestionGenerator.QuestionType.Division)
                        {
                            // increase max number by more on Division
                            firstMaxNumber += 10;
                            // don't increase min Number to avoid limiting division options.
                            secondMinNumber -= 2;
                        }
                    }
                    break;
                // if hard increase number ranges as shown below every turn and every 2 turns
                case Difficulty.Hard:
                    firstMaxNumber += 2;
                    firstMinNumber++;
                    secondMinNumber++;
                    secondMaxNumber += 2;
                    if (correctCount % 2 == 0)
                    {
                        firstMaxNumber += 4;
                        firstMinNumber += 2;
                        secondMinNumber += 2;
                        secondMaxNumber += 4;
                        if (questionType == QuestionGenerator.QuestionType.Division)
                        {
                            // increase max number by more on Division
                            firstMaxNumber += 15;
                            // don't increase min Number to avoid limiting division options.
                            secondMinNumber -= 4;
                        }
                    }
                    break;
                // default shouldn't be called, if so then difficulty was never set.
                default:
                    Console.WriteLine("Error Increasing Difficulty - No Match For Current Difficulty.");
                    Console.ReadLine();
                    break;

            }
        }
        // Ask the player if they wish to continue playing, if not then will set stillPlaying to false to break gameplay loop

        static int CalculateScore()
        {
            // calculate score based on difficulty.
            switch (currentDifficulty)
            {
                case Difficulty.Easy:
                    return correctCount + scoreBooster;
                case Difficulty.Medium:
                    return (correctCount * 2) + scoreBooster;
                case Difficulty.Hard:
                    return (correctCount * 3) + scoreBooster;
                default:
                    Console.WriteLine("Error checking difficulty to calculate score. Returning score based on Easy Difficulty.");
                    return correctCount;
            }
        }
        // set questionType and difficulty to None and then call OfferQuestionType() and then OfferDifficulty()
 
        public static void CloseApplication()
        {
            stillPlaying = false;
            Console.Clear();
            Console.WriteLine("Thank you for taking the time to play my game!\n\n -Daniel");
            ConsoleHandler.ShowLogo();
        }


    }
}
