using System;
using System.Collections.Generic;
using System.Text;

namespace MathForFun
{
    class GameManager
    {
        enum Difficulty { Easy, Medium, Hard };
        Difficulty currentDifficulty = Difficulty.Easy;
        bool stillPlaying;
        public bool StillPlaying
        {
            get { return stillPlaying; }
            set { stillPlaying = value; }
        }
        int firstMinNumber = 0;
        int firstMaxNumber = 1;
        int secondMinNumber = 1;
        int secondMaxNumber = 5;       
        int currentAnswer;
        int correctCount;

        public GameManager()
        {         
            StillPlaying = true;
            GreetPlayer();
            while(stillPlaying)
            {
                currentAnswer = QuestionGenerator.NewProblem(firstMinNumber, firstMaxNumber, secondMinNumber, secondMaxNumber);
                CheckAnswer();
            }
        }

        // if false game over and show score then offer to play again (only logic needed is resetting score)

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
                    Console.WriteLine($"\nYou answered correctly! Your score is now: {correctCount}.");
                    IncreaseMaxNumber();
                    OfferContinue();
                }
                else
                {
                    Console.WriteLine($"\nI'm sorry, {result} is not the correct answer.\n{QuestionGenerator.numOne} + {QuestionGenerator.numTwo} = {currentAnswer}.\n");
                    Console.WriteLine($"Your final score is {correctCount}\n");
                    OfferContinue();
                }
            }
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
                case 'y':
                case (char)ConsoleKey.Enter:
                case (char)ConsoleKey.Spacebar:
                    break;
                case 'n':
                    stillPlaying = false;
                    break;
                default:
                    Console.WriteLine("\n\nInvalid Entry. Please Select 'y' or 'n'.\n");
                    OfferContinue();
                    break;
            }
        }
        void IncreaseMaxNumber()
        {
            switch (currentDifficulty)
            {
                case Difficulty.Easy:
                    firstMaxNumber++;
                    if (correctCount % 3 == 0)
                    {
                        firstMinNumber++;
                        secondMinNumber++;
                        secondMaxNumber++; 
                    }
                    break;
                case Difficulty.Medium:
                    firstMaxNumber++;
                    firstMinNumber++;
                    if (correctCount % 3 == 0)
                    {
                        firstMaxNumber++;
                        firstMinNumber++;
                        secondMinNumber+=2;
                        secondMaxNumber+=2;
                    }
                    break;
                case Difficulty.Hard:
                    firstMaxNumber++;
                    firstMinNumber++;
                    secondMinNumber++;
                    secondMaxNumber++;
                    if (correctCount % 3 == 0)
                    {
                        firstMaxNumber+=3;
                        firstMinNumber+=3;
                        secondMinNumber += 3;
                        secondMaxNumber += 3;
                    }
                    break;
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
            Console.WriteLine("Please select a difficulty by entering the\ncorresponding number.\n\n1) Easy\n2) Medium\n3) Hard\n");
            switch(Console.ReadKey().KeyChar)
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
                default:
                    Console.WriteLine("Invalid response, Please enter a number 1-3.");
                    Console.ReadKey();
                    Console.Clear();
                    OfferDifficulty();
                break;
            }
        }

    }
}
