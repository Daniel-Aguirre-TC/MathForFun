using System;
using System.Collections.Generic;
using System.Text;

namespace MathForFun
{

    public class GameManager
    {
        // Variables //

        // difficulty will adjust how often the number range increases, and by how much. Score is also dependent on difficulty.
        enum Difficulty {None, Easy, Medium, Hard };
        // setting difficulty and questionType to none until player selects a difficulty/type.
        Difficulty currentDifficulty = Difficulty.None;
        QuestionGenerator.QuestionType questionType = QuestionGenerator.QuestionType.None;
        // stillPlaying is used to keep while loop running for gameplay
        bool stillPlaying;
        // numbers below are starting point for min/max values of opperands. These are increased throughout gameplay.
        int firstMinNumber;
        int firstMaxNumber;
        int secondMinNumber;
        int secondMaxNumber;   
        // stores the correct answer for the current question
        int currentAnswer;
        // correctCount is used to display score. Increases by one per correct answer.
        int correctCount;
        // scoreBooster will increase the score the higher the sum of currentAnswer is. This allows you to get more
        // points for more difficult questions, not just the difficulty selected.
        int scoreBooster = 0;

        // Constructor//

        // GameManager Constructor is called in Main at startup. 
        public GameManager()
        {         
            // stillPlaying bool to keep gameplay loop running in while statement below.
            stillPlaying = true;
            ShowLogo();
            GreetPlayer();
            // gameplay loop based on stillPlaying - this bool is changed by OfferContinue() method
            while(stillPlaying)
            {
                // generate new question and assign the answer to currentAnswer
                currentAnswer = QuestionGenerator.NewProblem(questionType, firstMinNumber, firstMaxNumber, secondMinNumber, secondMaxNumber);
                CheckAnswer();
            }
        }

        // Methods //

        // Print logo to screen - Press any key to begin/exit based on stillPlaying bool
        void ShowLogo()
        {
            // Logo, cause why not?
            Console.Clear();
            Console.WriteLine("\n\n______    ______   ____ ");
            Console.WriteLine("|    |    |       |    \\");
            Console.WriteLine("|    |    |       |     \\");
            Console.WriteLine("------    |-----  |     |");
            Console.WriteLine("|     \\   |       |     |");
            Console.WriteLine("|      \\  |_____  |_____|\n");

            Console.WriteLine("______    ______   _____   _     ");
            Console.WriteLine("|    |    |     |    |    | \\   |");
            Console.WriteLine("|    |    |     |    |    |  \\  |");
            Console.WriteLine("------    |-----|    |    |   \\ |");
            Console.WriteLine("|     \\   |     |    |    |    \\|");
            Console.WriteLine("|      \\  |     |  __|__  |     |");

            // will say press any key to begin or exit depending on if starting or ending game.
            string beginOrExit = stillPlaying ? "Begin" : "Exit";
            Console.WriteLine($"\n\nPress Any Key To {beginOrExit}.");
            ClearAfterReadKey();
        }
        // Introduction message with gameplay rules, after key press will Clear and OfferChangeSettings
        void GreetPlayer()
        {
            Console.WriteLine($"Thanks for playing my MathForFun game!\n\nCreated by Daniel Aguirre\n\nThe rules are simple, I'll provide you with \nrandom math problems, and you try to answer\nas many as you can correctly.\n\nThe difficulty will increase as your score\nincreases.If you get an answer wrong you can\ncontinue playing but your score will reset.\n\nYour score will be determined by\nyour difficulty and the problem presented.\n\nPlease press any key to continue.");
            ClearAfterReadKey();
            OfferChangeSettings();
        }
        // Ask the user what type of math question to present then assign questionType
        void OfferQuestionType()
        {
            while (questionType == QuestionGenerator.QuestionType.None)
            {
                Console.Clear();
                Console.WriteLine("Please select a category by entering the\ncorresponding number.\n\n1) Addition\n2) Subtraction\n3) Multiplication\n4) Division\n");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        questionType = QuestionGenerator.QuestionType.Addition;
                        break;
                    case '2':
                        questionType = QuestionGenerator.QuestionType.Subtraction;
                        break;
                    case '3':
                        questionType = QuestionGenerator.QuestionType.Multiplication;
                        break;
                    case '4':
                        questionType = QuestionGenerator.QuestionType.Division;
                        break;
                    // if default, invalid response, so no difficulty set which will cause while loop to re-run.
                    default:
                        Console.WriteLine("Invalid response, Please enter a number 1-3.");
                        ClearAfterReadKey();
                        break;
                }
            }
            Console.WriteLine($"\n\nYou have selected the category: {questionType}\nPress any key to continue.");
            ClearAfterReadKey();
        }
        // Ask the user what difficulty they want and then assign currentDifficulty
        void OfferDifficulty()
        {
            // assign initial values for number range at start of game

            // use while loop to offer difficulty again if player inputs invalid entry. This prevents user from continuing without setting a difficulty.
            while (currentDifficulty == Difficulty.None)
            {
                Console.WriteLine("Please select a difficulty by entering the\ncorresponding number.\n\n1) Easy\n2) Medium\n3) Hard\n");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        currentDifficulty = Difficulty.Easy;
                        // assign initial number range based on difficulty. This will be updated as gameplay continues but will be reset if we get a wrong answer.
                        firstMinNumber = 0;
                        firstMaxNumber = 5;
                        secondMinNumber = 1;
                        secondMaxNumber = 5;
                        if (questionType == QuestionGenerator.QuestionType.Division)
                        {
                            firstMaxNumber += 25;
                        }
                        break;
                    case '2':
                        currentDifficulty = Difficulty.Medium;
                        firstMinNumber = 0;
                        firstMaxNumber = 10;
                        secondMinNumber = 1;
                        secondMaxNumber = 10;
                        if (questionType == QuestionGenerator.QuestionType.Division)
                        {
                            firstMaxNumber += 50;
                        }
                        break;
                    case '3':
                        currentDifficulty = Difficulty.Hard;
                        currentDifficulty = Difficulty.Medium;
                        firstMinNumber = 0;
                        firstMaxNumber = 15;
                        secondMinNumber = 1;
                        secondMaxNumber = 15;
                        if (questionType == QuestionGenerator.QuestionType.Division)
                        {
                            firstMaxNumber += 75;
                        }
                        break;
                    // if default, invalid response, so no difficulty set which will cause while loop to re-run.
                    default:
                        Console.WriteLine("Invalid response, Please enter a number 1-3.");
                        ClearAfterReadKey();
                        break;
                }
            }
            Console.WriteLine($"\n\nYou have selected the difficulty: {currentDifficulty}\nPress any key to continue.");
            ClearAfterReadKey();
        }
        // After user presses any key, clear the screen
        void ClearAfterReadKey()
        {
            Console.ReadKey();
            Console.Clear();
        }
        // Check if answer was right, offer to continue
        void CheckAnswer()
        {
            // if can parse set result
            if (int.TryParse(Console.ReadLine(), out int result))
            {
                // if result is correct
                if (result == currentAnswer)
                {
                    // score booster is based on numOne + numTwo so that subtraction score increases with higher numbers even if difference is  low number.
                    scoreBooster += Convert.ToInt32(Math.Floor((QuestionGenerator.numOne + QuestionGenerator.numTwo) / 10m));
                    // increase correctCount, notify player, offer continue.
                    correctCount++;
                    // Notify user their answer was correct, increase Range of possible opperands for next question, offer continue playing
                    Console.WriteLine($"\nYou answered correctly! Your score is now: {CalculateScore()}.\n\nYour total correct answers so far is {correctCount}\n");
                    IncreaseNumberRange();
                    OfferContinue();
                }
                // else means answer given was incorrect
                else
                {
                    // notify player they lost and offer continue.
                    Console.WriteLine($"\nI'm sorry, {result} is not the correct answer.\n\n{QuestionGenerator.numOne} {QuestionGenerator.currentOperator} {QuestionGenerator.numTwo} = {currentAnswer}.\n");
                    Console.WriteLine($"Your final score is {CalculateScore()}, with {correctCount} correct answers.\n");
                    // reset correctCount to 0 if got wrong answer.
                    correctCount = 0;
                    scoreBooster = 0;
                    OfferContinue();
                }
            }
            // else the parse failed so CheckAnswer again to get a new entry
            else
            {
                Console.WriteLine($"Invalid entry. Please enter a number.");
                CheckAnswer();
            }
        }
        // increase number range that problems are created from
        void IncreaseNumberRange()
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
        void OfferContinue()
        {
            Console.WriteLine("Do you wish to continue playing? Please enter y/n\n");
            switch (Console.ReadKey().KeyChar)
            {
                // if yes, spacebar, or enter key then continue
                case 'y':
                case (char)ConsoleKey.Enter:
                case (char)ConsoleKey.Spacebar:
                    if (correctCount == 0)
                    {
                        // set questionType and currentDifficulty back to none so that we can offer new settings if correctCount == 0
                        OfferChangeSettings();
                    }
                    break;
                // if n then set stillPlaying to false to break gameplay loop.
                case 'n':
                    CloseApplication();
                    break;
                // if anything else then invalid entry and offer continue again.
                default:
                    Console.WriteLine("\n\nInvalid Entry. Please Select 'y' or 'n'.\n");
                    OfferContinue();
                    break;
            }
        }
        // return the players score, logic dependant on difficulty and the sum of the opperands for the problem solved.
        int CalculateScore()
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
        void OfferChangeSettings()
        {
            questionType = QuestionGenerator.QuestionType.None;
            currentDifficulty = Difficulty.None;
            OfferQuestionType();
            OfferDifficulty();
        }
        // set stillPlaying to false, Clear the screen, thank player for playing, then display Logo
        void CloseApplication()
        {
            stillPlaying = false;
            Console.Clear();
            Console.WriteLine("Thank you for taking the time to play my game!\n\n -Daniel");
            ShowLogo();
        }
    }
}
