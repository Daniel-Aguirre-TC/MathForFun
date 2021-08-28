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
        public static Difficulty currentDifficulty = Difficulty.None;

        // questionType will determine the math category of questions we present.
        public enum QuestionType { None, Addition, Subtraction, Multiplication, Division }
        public static QuestionType questionType = QuestionType.None;

        // stillPlaying is used to keep while loop running for gameplay
        public static bool stillPlaying;

        // numbers below are starting point for min/max values of opperands. These are increased throughout gameplay.
        public static int firstMinNumber;
        public static int firstMaxNumber;
        public static int secondMinNumber;
        public static int secondMaxNumber;

        // lastNumOne and lastNumTwo used to prevent showing the same answer twice.
        static int lastNumOne;
        static int lastNumTwo;

        // numOne and numTwo represent the opperands in the current math equation being presented.
        public static int numOne;
        public static int numTwo;

        // stores the correct answer for the current question
        public static int currentAnswer;

        // turnCount is used to calculate score. Increases by one per correct answer. (in two player it increases once after both players have had their turn)
        public static int turnCount;

        // scoreBooster will increase the score the higher the sum of currentAnswer is. This allows you to get more
        // points for more difficult questions, not just the difficulty selected.
        public static int scoreBooster = 0;

        public static int playerOneScore;
        public static int playerTwoScore;

        public static void StartGame()
        {
            // stillPlaying bool to keep gameplay loop running in while statement below.
            stillPlaying = true;
            turnCount = 1;
            ConsoleHandler.ShowLogo();
            ConsoleHandler.GreetPlayer();
            InputHandler.OfferChangeSettings();

            // each loop will represent one turn in-game.
            while(stillPlaying)
            {

                // regardless of if we are playing single player or two, we will set numbers at the start of each turn.

                // update numOne and numTwo
                SetNumbers(questionType, firstMinNumber, firstMaxNumber, secondMinNumber, secondMaxNumber);
                if (playerCount == NumberOfPlayers.Two)
                {
                    // generate new question and assign the answer to p1QuestionSlot or p2QuestionSlot depending on whose turn it is
                    ConsoleHandler.DrawSplitScreen();
                    // check answer and add to score depending on whose turn it is
                    InputHandler.CheckAnswer();
                }
                else
                {
                    ConsoleHandler.DrawSinglePlayer();
                    InputHandler.CheckAnswer();
                }


            }
        }

        static void SetNumbers(QuestionType questionType, int firstNumberMin, int firstNumberMax, int secondNumberMin, int secondNumberMax)
        {
            var random = new Random();
            numOne = random.Next(firstNumberMin, firstNumberMax);
            numTwo = random.Next(secondNumberMin, secondNumberMax);

            switch (questionType)
            {
                // if category is addition or multiplication and it's the same as last problem, generate new numbers.
                case QuestionType.Addition:
                case QuestionType.Multiplication:
                    if ((numOne == lastNumOne && numTwo == lastNumTwo) || (numOne == lastNumTwo && numTwo == lastNumOne))
                    {
                        SetNumbers(questionType, firstNumberMin, firstNumberMax, secondNumberMin, secondNumberMax);
                    }
                    else { UpdateLastNums(); }
                    break;
                // if category is subtration
                case QuestionType.Subtraction:
                    // prevent duplicate question from last one, and prevent negative answers, and prevent having two questions in a row with same answer.
                    if (numTwo > numOne || (numOne == lastNumOne && numTwo == lastNumTwo) || numOne - numTwo == lastNumOne - lastNumTwo)
                    { SetNumbers(questionType, firstNumberMin, firstNumberMax, secondNumberMin, secondNumberMax); }
                    else { UpdateLastNums(); }
                    break;
                // if category is Divison and is same numTwo as last problem or modulus != 0 (to avoid decimals), or numTwo is 1 (too easy) or numOne is less than numTwo
                case QuestionType.Division:
                    if (numTwo == lastNumTwo || numTwo == 1 || numOne % numTwo != 0 || numOne < numTwo)
                    { SetNumbers(questionType, firstNumberMin, firstNumberMax, secondNumberMin, secondNumberMax); }
                    else { UpdateLastNums(); }
                    break;
            }

            switch(questionType)
            {
                case QuestionType.Addition:
                    currentAnswer = numOne + numTwo;
                    break;
                case QuestionType.Multiplication:
                    currentAnswer = numOne * numTwo;
                    break;
                case QuestionType.Subtraction:
                    currentAnswer = numOne - numTwo;
                    break;              
                case QuestionType.Division:
                    currentAnswer = numOne / numTwo;
                    break;
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
        }

        static void UpdateLastNums()
            {
                lastNumOne = numOne;
                lastNumTwo = numTwo;
            }

        public static void IncreaseNumberRange()
        {

            // increase range based on difficulty
            switch (currentDifficulty)
            {
                // if easy increase number ranges as shown below every turn and every four turns.
                case Difficulty.Easy:                        
                    firstMaxNumber++;
                    if (GameManager.turnCount % 4 == 0)
                    {
                        firstMinNumber++;
                        secondMinNumber++;
                        secondMaxNumber++;
                        if (questionType == GameManager.QuestionType.Division)
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
                    if (turnCount % 3 == 0)
                    {
                        firstMinNumber++;
                        firstMaxNumber++;
                        secondMinNumber += 2;
                        secondMaxNumber += 2;
                        if (questionType == GameManager.QuestionType.Division)
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
                    if (turnCount % 2 == 0)
                    {
                        firstMaxNumber += 4;
                        firstMinNumber += 2;
                        secondMinNumber += 2;
                        secondMaxNumber += 4;
                        if (questionType == GameManager.QuestionType.Division)
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

            // return new score based on current score, current turn, difficulty, and sum of two opperands use to create current question
            public static int CalculateScore(int currentScore)
            {

            int multiplayerModifier = 1;
            if (playerCount == NumberOfPlayers.Two)
            {
                multiplayerModifier = 2;
            }

            // calculate score based on difficulty.
            switch (currentDifficulty)
                {
                    case Difficulty.Easy:
                        return  Convert.ToInt32(Math.Ceiling((double)turnCount/multiplayerModifier) + scoreBooster + currentScore);
                    case Difficulty.Medium:
                        return (Convert.ToInt32(Math.Ceiling((double)turnCount / multiplayerModifier) * 2) + scoreBooster + currentScore);
                    case Difficulty.Hard:
                        return (Convert.ToInt32(Math.Ceiling((double)turnCount / multiplayerModifier) * 3) + scoreBooster + currentScore);
                    default:
                        Console.WriteLine("Error checking difficulty to calculate score. Returning score based on Easy Difficulty.");
                        return turnCount;
                }
            }
            // set questionType and difficulty to None and then call OfferQuestionType() and then OfferDifficulty()

            public static void CloseApplication()
            {
                stillPlaying = false;
                Console.Clear();
                ConsoleHandler.CenterMidScreenAndPrintArray(new string[] { " Thank you for taking the time to play my game!\n", "-Daniel" });                
                ConsoleHandler.ShowLogo();
            }
        
        
        
        

    }
}
