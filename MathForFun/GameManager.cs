using System;
using System.Collections.Generic;
using System.Text;

namespace MathForFun
{
    class GameManager
    {
        // difficulty will adjust how often the number range increases, and by how much.
        enum Difficulty {None, Easy, Medium, Hard };
        // setting difficulty to none until player selects a difficulty.
        Difficulty currentDifficulty = Difficulty.None;

        // 
        bool stillPlaying;

        // numbers below are starting point for min/max values of opperands. These are increased throughout gameplay.
        int firstMinNumber = 0;
        int firstMaxNumber = 1;
        int secondMinNumber = 1;
        int secondMaxNumber = 5;   
        // stores the correct answer for the current question
        int currentAnswer;
        // correctCount is used to display score. Increases by one per correct answer.
        int correctCount;

        // GameManager Constructor is called in Main at startup. 
        public GameManager()
        {         
            // stillPlaying bool to keep gameplay loop running in while statement below.
            stillPlaying = true;
            ShowLogo();
            GreetPlayer();
            while(stillPlaying)
            {
                currentAnswer = QuestionGenerator.NewProblem(firstMinNumber, firstMaxNumber, secondMinNumber, secondMaxNumber);
                CheckAnswer();
            }
        }

        void CheckAnswer()
        {
            // if can parse set result
            if (int.TryParse(Console.ReadLine(), out int result))
            {
                // if result is correct
                if (result == currentAnswer)
                {
                    // increase correctCount, notify player, offer continue.
                    correctCount++;
                    Console.WriteLine($"\nYou answered correctly! Your score is now: {correctCount}.\n");
                    IncreaseNumberRange();
                    OfferContinue();
                }
                // else means answer given was incorrect
                else
                {
                    // notify player they lost and offer continue.
                    Console.WriteLine($"\nI'm sorry, {result} is not the correct answer.\n\n{QuestionGenerator.numOne} + {QuestionGenerator.numTwo} = {currentAnswer}.\n");
                    Console.WriteLine($"Your final score is {correctCount}.\n");
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
        void OfferContinue()
        {
            Console.WriteLine("Do you wish to continue playing? Please enter y/n\n");
            switch (Console.ReadKey().KeyChar)
            {
                // if yes, spacebar, or enter key then continue
                case 'y':
                case (char)ConsoleKey.Enter:
                case (char)ConsoleKey.Spacebar:
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
                        secondMinNumber+=2;
                        secondMaxNumber+=2;
                    }
                    break;
                // if hard increase number ranges as shown below every turn and every 2 turns
                case Difficulty.Hard:
                    firstMaxNumber+=2;
                    firstMinNumber++;
                    secondMinNumber++;
                    secondMaxNumber+=2;
                    if (correctCount % 2 == 0)
                    {
                        firstMaxNumber+=4;
                        firstMinNumber+=2;
                        secondMinNumber += 2;
                        secondMaxNumber += 4;
                    }
                    break;
                // default shouldn't be called, if so then difficulty was never set.
                default:
                    Console.WriteLine("Error Increasing Difficulty - No Match For Current Difficulty.");
                    Console.ReadLine();
                    break;

            }
        }
        void GreetPlayer()
        {
            Console.WriteLine($"Thanks for playing my MathForFun game!\n\nCreated by Daniel Aguirre\n\nThe rules are simple, I'll provide you with \nrandom addition problems, and you try to answer\nas many as you can correctly.\n\nThe difficulty will increase as your score\nincreases.If you get an answer wrong you can\ncontinue playing but your score will reset.\n\n");
            OfferDifficulty();
            Console.WriteLine($"\n\nYou have selected the difficulty: {currentDifficulty}\nPress any key to continue.");
            Console.ReadKey();
        }
        void OfferDifficulty()
        {
            // use while loop to offer difficulty again if player inputs invalid entry. This prevents user from continuing without setting a difficulty.
            while (currentDifficulty == Difficulty.None)
            {
                Console.WriteLine("Please select a difficulty by entering the\ncorresponding number.\n\n1) Easy\n2) Medium\n3) Hard\n");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        currentDifficulty = Difficulty.Easy;
                        break;
                    case '2':
                        currentDifficulty = Difficulty.Medium;
                        break;
                    case '3':
                        currentDifficulty = Difficulty.Hard;
                        break;
                    // if default, invalid response, so no difficulty set which will cause while loop to re-run.
                    default:
                        Console.WriteLine("Invalid response, Please enter a number 1-3.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }
        void ShowLogo()
        {
            // Logo, cause why not?

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
            Console.ReadKey();
            Console.Clear();
        }
        void CloseApplication()
        {
            stillPlaying = false;
            Console.Clear();
            Console.WriteLine("Thank you for taking the time to play my game!\n\n -Daniel");
            ShowLogo();
        }
    }
}
